﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Diagnostics;


namespace GigaBike
{
    public class Controller
    {
        public Login Login { get; }
        public Catalog Catalog { get; }
        public Order Order { get; }
        public DataBase DataBase { get; }
        public Planning Planning { get; }
        private List<Order> ordersRegistered;
        private List<int> IdPurchaseOrderParts;
        public Stock Stock { get; }
        public OrderPartPage OrderPartPage { get; }
        public BikeInStock BikeInStock { get; }

        public Controller()
        {
            this.DataBase = new DataBase();
            this.Login = new Login(this.DataBase);
            this.Catalog = new Catalog(this.DataBase);
            this.Order = new Order(this.DataBase);
            this.Planning = new Planning(this.DataBase);
            ordersRegistered = new List<Order>();
            this.Stock = new Stock(this.DataBase);
            this.BikeInStock = new BikeInStock(this.DataBase);
        }

        public void Init()
        {
            DataBase.Connect();
        }

        public void SaveOrderAndSlotInDatabase()
        {
            Order.SaveInDatabase();
            Planning.SaveSlotOfIdOrderToDatabase(Order.IdOrder);
        }

        public void SaveOrderInformation(Customer customer)
        {
            Order.SaveCustomer(customer);
        }

        public void SetCurrentIdOrder()
        {
            MySqlDataReader reader = DataBase.GetNextIdOrder();
            if (reader.Read())
            {
                if (reader.IsDBNull(0) == false)
                    Order.IdOrder = reader.GetInt32(0);
                else
                    Order.IdOrder = 1;
            }
            reader.Close();
        }
        public void SetDeliveryDateOrder()
        {
            Planning.RefreshFromDatabase();
            SetCurrentIdOrder();
            SetDateForOrderBike();
            Order.DeliveryDate = Planning.GetDeliveryDate(Order.IdOrder);
        }

        public void SetDateForOrderBike()
        {
            Planning.SetSlotForBikeOrder(Order);
        }

        public void RefreshOrderAndPlanningFromDatabase()
        {
            Planning.RefreshFromDatabase();
            GetOrdersFromDatabase();
            GetOrderModelsFromDatabase();
        }

        public void UpdatePlanningAfterUserUpdate(List<Order> ordersShown)
        {
            foreach (Order currentOrder in ordersShown)
                currentOrder.Bikes.ForEach(bike => Planning.UpdateSlotsInDatabaseByBikeOrder(bike));

            Planning.DeleteTheSlotUnusedFromTheDatabase();
        }

        public List<Order> OrdersRegistered
        {
            get
            {
                return new List<Order>(ordersRegistered).ConvertAll(o => new Order(o));
            }
        }

        public void BindBikeToNewSLot(int idOrder, int idOrderModel, DateTime deliveryDate, int numberSlot)
        {
            Order currentOrder = ordersRegistered.Find(o => o.IdOrder == idOrder);

            if (currentOrder is null) return;

            BikeOrder currentBikeOrder = currentOrder.Bikes.Find(b => b.IdOrderModel == idOrderModel);

            if (currentBikeOrder is null) return;

            List<Slot> slotToBind = new List<Slot>();

            Planning.UnbindSlotByIdOrderModel(idOrderModel);

            Planning.BindSlotToIdOrderModelByDuration(idOrder, idOrderModel, deliveryDate, numberSlot);
            Planning.BindBikeOrderToExistingSlot(currentBikeOrder);
        }

        public void SetPartsToOrder()
        {
            // Get every busy slots
            List<Slot> busySlots = Planning.GetAllBusySlot();

            // Clear the current order purchase
            Stock.PurchaseOrderPartHandler.ClearCurrentPurchase();

            // Get the bike for every busy slot
            foreach (Slot slot in busySlots)
            {
                Order order = ordersRegistered.Find(o => o.IdOrder == slot.IdOrder);

                if (order is not null)
                {
                    BikeOrder bikeOrder = order.Bikes.Find(b => b.IdOrderModel == slot.IdOrderModel);
                    int idBike = bikeOrder.Bike.IdBike;
                    List<BikePart> partsOfBike = Stock.PartToModelLinker.GetPartsForIdModel(idBike);

                    // Add the part unsufficient in stock
                    foreach (BikePart part in partsOfBike)
                    {
                        int partToOrder = Math.Max(0, part.QuantityForBike/* - part.Part.QuantityOrdered - part.Part.QuantityInStock*/);

                        if (partToOrder > 0)
                            Stock.PurchaseOrderPartHandler.AddPartToCurrentPurchase(part.Part, partToOrder);
                    }
                }
            }

            // Substract the quantity to order with the parts in stock
            foreach (OrderPart orderPart in Stock.PurchaseOrderPartHandler.CurrentPurchase.OrderParts)
            {
                int quantityInStock = Stock.GetQuantityInStockForPart(orderPart.Part);
                orderPart.QuantityToOrder = Math.Max(0, orderPart.QuantityToOrder - quantityInStock - orderPart.Part.QuantityOrdered);
            }
        }

        // TODO: Clean
        private void GetOrdersFromDatabase()
        {
            MySqlDataReader reader = DataBase.GetOrders();

            ordersRegistered.Clear();

            while (reader.Read())
            {
                int idOrder = reader.GetInt32(0);
                DateTime deliveryDate = reader.GetDateTime(1);
                string nameCustomer = reader.GetString(2);
                string addressCustomer = reader.GetString(3);
                string tvaCustomer = reader.GetString(4);
                string phoneCustomer = reader.GetString(5);

                Customer orderCustomer = new Customer();
                orderCustomer.Name = nameCustomer;
                orderCustomer.Address = addressCustomer;
                orderCustomer.TVA = tvaCustomer;
                orderCustomer.Phone = phoneCustomer;

                Order currentOrder = new Order(DataBase);
                currentOrder.IdOrder = idOrder;
                currentOrder.DeliveryDate = deliveryDate;
                currentOrder.SaveCustomer(orderCustomer);

                ordersRegistered.Add(currentOrder);
            }

            reader.Close();
        }

        private void GetOrderModelsFromDatabase()
        {
            MySqlDataReader reader = DataBase.GetOrdersModel();

            while (reader.Read())
            {
                int idOrder = reader.GetInt32(0);
                int idOrderModel = reader.GetInt32(1);
                int idModelBike = reader.GetInt32(2);
                string nameBike = reader.GetString(3);
                int idColor = reader.GetInt32(4);
                string nameColor = reader.GetString(5);
                int idSize = reader.GetInt32(6);
                string nameSize = reader.GetString(7);
                int priceBike = reader.GetInt32(8);
                int slotDurationBike = reader.GetInt32(9);

                Order currentOrder = ordersRegistered.Find(order => order.IdOrder == idOrder);
                Bike orderBike = new Bike(idModelBike, nameBike, priceBike, new Color(idColor, nameColor), new Size(idSize, nameSize), "", slotDurationBike);
                currentOrder.AddSingleBike(idOrderModel, orderBike);
            }

            reader.Close();

            // Binding to slot
            foreach (Order currentOrder in ordersRegistered)
            {
                foreach (BikeOrder currentBikeOrder in currentOrder.Bikes)
                    Planning.BindBikeOrderToExistingSlot(currentBikeOrder);
            }
        }

        public void OrderedReceived(PurchaseOrderPart purchase)
        {
            if (purchase.IsReceived)
                return;

            MySqlDataReader reader;

            foreach (OrderPart currentOrderPart in purchase.OrderParts)
            {
                reader = DataBase.AddPartModelToStock(currentOrderPart.Part.IdPart, currentOrderPart.QuantityToOrder);
                reader.Close();

                currentOrderPart.Part.DeleteQuantityOrdered(currentOrderPart.QuantityToOrder);
                currentOrderPart.Part.AddStockQuantity(currentOrderPart.QuantityToOrder);
                reader = DataBase.DeletePartOrdered(currentOrderPart.Part.IdPart, currentOrderPart.QuantityToOrder);
                reader.Close();
            }

            reader = DataBase.GetPurchaseOrderPart();

            IdPurchaseOrderParts = new List<int>();

            while (reader.Read())
            {
                if (purchase.IdPurchaseOrderPart == reader.GetInt32(1))
                    IdPurchaseOrderParts.Add(reader.GetInt32(0));
            }
            reader.Close();

            purchase.IsReceived = true;
            foreach (int IdPurchaseOrderPart in IdPurchaseOrderParts)
            {
                reader = DataBase.SetReadyStatePurchaseOrderPart(IdPurchaseOrderPart, true);
                reader.Close();
            }
        }

        public List<Customer> GetCustomerList()
        {
            List<Customer> customerList = new List<Customer>();

            // Get Customer from database
            MySqlDataReader reader = DataBase.GetAllCustomers();

            // Create Customer instances
            while (reader.Read())
            {
                Customer currentCustomer = new Customer();

                currentCustomer.TVA = reader.GetString(0);
                currentCustomer.Name = reader.GetString(1);
                currentCustomer.Address = reader.GetString(2);
                currentCustomer.Phone = reader.GetString(3);

                customerList.Add(currentCustomer);
            }

            reader.Close();

            return customerList;
        }
    }
}
