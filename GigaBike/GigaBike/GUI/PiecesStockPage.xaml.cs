﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GigaBike
{
    /// <summary>
    /// Logique d'interaction pour PiecesStockPage.xaml
    /// </summary>
    public partial class PiecesStockPage : Page {
        private Action goBackToStockCallback = null;
        private Action goToOrderPiecesCallback = null;

        public PiecesStockPage() {
            InitializeComponent();
        }

        private void TableRecapCommand(object sender, SelectionChangedEventArgs e) {

        }

        public void SetStockList(Stock CurrentPiece)
        {
            List<StockListGrid> stockRecap = new List<StockListGrid>();

            foreach (Part currentPiece in CurrentPiece.Parts)
            {
                StockListGrid currentStockRecapGrid = new StockListGrid();

                // currentStockRecapGrid.IdPart = currentPiece.IdPart;
                currentStockRecapGrid.Name = currentPiece.NamePart;
                currentStockRecapGrid.QuantityInStock = currentPiece.QuantityInStock;
                currentStockRecapGrid.QuantityOrdered = currentPiece.QuantityOrdered;
                currentStockRecapGrid.Color = currentPiece.Color;
                currentStockRecapGrid.Size = currentPiece.Size;
                // currentStockRecapGrid.Threshold = currentPiece.Threshold;
                currentStockRecapGrid.Location = currentPiece.Location;

                stockRecap.Add(currentStockRecapGrid);
            }

            TableRecap.ItemsSource = stockRecap;
        }

        private void ButtonOrderPiece(object sender, RoutedEventArgs e) {
            if (goToOrderPiecesCallback is not null) goToOrderPiecesCallback();
        }

        private void ButtonGoBackToStock(object sender, RoutedEventArgs e) {
            if (goBackToStockCallback is not null) goBackToStockCallback();
        }

        public Action GoBackToStockCallback {
            set {
                goBackToStockCallback = value;
            }
        }

        public Action GoToOrderPiecesCallback {
            set {
                goToOrderPiecesCallback = value;
            }
        }

        public class StockListGrid
        {
            // public int IdPart { get; set; }
            public string Name { get; set; }
            public int QuantityOrdered { get; set; }
            public int QuantityInStock { get; set; }
            public Color Color { get; set; }
            public Size Size { get; set; }
            // public int Threshold { get; set; }
            public int Location { get; set; }

        }

        private void TableRecap_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
