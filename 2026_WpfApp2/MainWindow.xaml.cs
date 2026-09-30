using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace _2026_WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            // 品項、單價與 Slider 對應
            var items = new (string DisplayName, int Price, Slider QtySlider)[]
            {
                ("紅茶大杯", 60, sliderQtyRedLarge),
                ("紅茶小杯", 40, sliderQtyRedSmall),
                ("綠茶大杯", 60, sliderQtyGreenLarge),
                ("綠茶小杯", 40, sliderQtyGreenSmall),
                ("可樂大杯", 50, sliderQtyColaLarge),
                ("可樂小杯", 30, sliderQtyColaSmall),
            };

            var sb = new StringBuilder();
            int originalTotal = 0; // 記錄折扣前的原價總額
            bool anyOrdered = false;

            foreach (var item in items)
            {
                // 直接轉為整數數量
                int qty = (int)item.QtySlider.Value;

                if (qty == 0)
                {
                    continue;
                }

                anyOrdered = true;
                int lineTotal = qty * item.Price;
                originalTotal += lineTotal;
                sb.AppendLine($"{item.DisplayName} × {qty} ＝ {lineTotal} 元");
            }

            if (!anyOrdered)
            {
                SummaryTextBlock.Text = "您尚未訂購任何品項。";
            }
            else
            {
                // 折扣判斷邏輯
                double discountRate = 1.0;
                string discountName = "無折扣";

                if (originalTotal >= 500)
                {
                    discountRate = 0.8;
                    discountName = "滿 500 元享 8 折";
                }
                else if (originalTotal >= 300)
                {
                    discountRate = 0.85;
                    discountName = "滿 300 元享 85 折";
                }
                else if (originalTotal >= 200)
                {
                    discountRate = 0.9;
                    discountName = "滿 200 元享 9 折";
                }

                // 計算折扣後金額 (使用 Math.Round 四捨五入至整數)
                int finalTotal = (int)Math.Round(originalTotal * discountRate);

                sb.AppendLine("----------------------------");
                sb.AppendLine($"原價小計：{originalTotal} 元");
                sb.AppendLine($"優惠活動：{discountName}");
                sb.AppendLine($"總計：{finalTotal} 元");

                SummaryTextBlock.Text = sb.ToString();
            }
        }
    }
}