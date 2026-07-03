using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pizza_Project
{
    public partial class Maininterface : Form
    {
        public Maininterface()
        {
            InitializeComponent();
        }

        private int TotalPrice = 0;
        void CalculateTotalPrice(int Price)
        {
            TotalPrice += Price;
            laTotalPrice.Text = $"${TotalPrice}";
        }

        void ResetOrder()
        {
            laTotalBill.Text = string.Empty;
            laTotalPrice.Text = "$0.00";
            TotalPrice = 0;
            rBEatin.Checked = true;
        }

        bool OrderEmpty()
        {
            if(laTotalBill.Text == string.Empty)
            {
                return true;
            }
            return false;
        }

        bool isFirstOrder()
        {
            if(laTotalBill.Text.Length > 0)
            {
               return true;
            }

            return false;
        }

        public void OrderBill(OrderItem OrderSummary)
        {
            if(isFirstOrder())
            {
                laTotalBill.Text += "\n-------------------------------\n";
            }
            
           laTotalBill.Text+= OrderSummary.GetOrderSummary();
            CalculateTotalPrice(OrderSummary.Price);
        }


        private void pBPizzaMenu_Click(object sender, EventArgs e)
        {
            Form PizzaMenue = new PizzaForm(this);
            PizzaMenue.ShowDialog();
        }

        private void pBBurgerMenu_Click(object sender, EventArgs e)
        {
            Form BergerMenue = new BurgerForm(this);
            BergerMenue.ShowDialog();
        }

        private void pBDrinksandSidesMenu_Click(object sender, EventArgs e)
        {
            Form Drinks_and_SidesMenue = new DrinksForm(this);
            Drinks_and_SidesMenue.ShowDialog();
        }

        private void PlaceOrder_Click(object sender, EventArgs e)
        {
            if (OrderEmpty())
            {
                MessageBox.Show("Order is Empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult Result = MessageBox.Show("Are you sure?", "Confirmt", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (Result == DialogResult.OK)
            {
                MessageBox.Show("Order Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            ResetOrder();
        }

        private void btPlaceOrder_MouseEnter(object sender, EventArgs e)
        {
            btPlaceOrder.ForeColor = Color.White;
        }

        private void btPlaceOrder_MouseLeave(object sender, EventArgs e)
        {
            btPlaceOrder.ForeColor = Color.LightSeaGreen;
        }

        private void btResetOrder_Click(object sender, EventArgs e)
        {
            ResetOrder();
        }

        private void btResetOrder_MouseEnter(object sender, EventArgs e)
        {
            btResetOrder.ForeColor = Color.White;
        }

        private void btResetOrder_MouseLeave(object sender, EventArgs e)
        {
            btResetOrder.ForeColor = Color.LightSeaGreen;
        }
    }
}
