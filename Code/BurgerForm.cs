using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pizza_Project
{
 
    public partial class BurgerForm : Form
    {
        private Maininterface _maininterFace;

       private struct stBerger 
        {
           public string Type;
           public string Size;
           public string Toppings;
        }
       stBerger Berger = new stBerger();

        public BurgerForm(Maininterface maineinterFace)
        {
            InitializeComponent();
            _maininterFace = maineinterFace;
        }

        int CalculateTypePrice()
        {
            if(rbBeef.Checked)
            {
                Berger.Type = rbBeef.Text;
                return Convert.ToInt16(rbBeef.Tag);
                
            }
            
            if(rbCh.Checked)
            {
                Berger.Type = rbCh.Text;
                return Convert.ToInt16(rbCh.Tag);
            }

            if(rbMuBu.Checked)
            {
                Berger.Type = rbMuBu.Text;
                return Convert.ToInt16(rbMuBu.Tag);
            }

            return 0;
        }

        int CalculateSizePrice()
        {
            if (rbSin.Checked)
            {
                Berger.Size = rbSin.Text;
                return Convert.ToInt16(rbSin.Tag);
            }

            if (rbDou.Checked)
            {
                Berger.Size = rbDou.Text;
                return Convert.ToInt16(rbDou.Tag);
            }

            if (rbTr.Checked)
            {
                Berger.Size = rbTr.Text;
                return Convert.ToInt16(rbTr.Tag);
            }

            return 0;
        }

        int CalculateToppingsPrice()
        {
            int Price = 0;
            string Toppings = string.Empty;
            if (chCh.Checked)
            {
                Toppings += chCh.Text + ", ";
                Price += Convert.ToInt16(chCh.Tag);
            }

            if(chBa.Checked)
            {
                Toppings += chBa.Text + ", ";
                Price += Convert.ToInt16(chBa.Tag);
            }

            if(chPi.Checked)
            {
                Toppings += chPi.Text + ", ";
                Price += Convert.ToInt16(chPi.Tag);
            }

            if(chSp.Checked)
            {
                Toppings += chSp.Text + ", "; 
                Price += Convert.ToInt16(chSp.Tag);
            }
            Berger.Toppings = Toppings.TrimEnd(',',' ');
            return Price;
        }

        int CalculateTotalPrice()
        {
            int TotalPrice = CalculateTypePrice() + CalculateSizePrice() + CalculateToppingsPrice();
            return Convert.ToInt16(Qn.Value) * TotalPrice;
        }

        void PrintTotalPrice()
        {
            
               laTotalPrice.Text = "$" + CalculateTotalPrice();
             
        }

        void ResetButtons()
        {
            rbBeef.Checked = true;
            rbSin.Checked = true;


            chCh.Checked = false;
            chBa.Checked = false;
            chPi.Checked = false;
            chSp.Checked = false;

            Qn.Value = 1;

            PrintTotalPrice();
        }

        bool ChecktheOrder()
        {
            if (CalculateTotalPrice() == 0)
            {
                return true;
            }

            return false;
        }

        private void Type_Change(object sender, EventArgs e)
        {
            PrintTotalPrice();
        }

        private void SizeChange(object sender, EventArgs e)
        {
            PrintTotalPrice();
        }

        private void ToppingsChange(object sender, EventArgs e)
        {
            PrintTotalPrice();
        }

         private void Qn_ValueChanged(object sender, EventArgs e)
        {
            PrintTotalPrice();
        }

        private void BurgarForm_Load(object sender, EventArgs e)
        {
            ResetButtons();
        }

        private void btReset_Click(object sender, EventArgs e)
        { 
            ResetButtons();
        }

        private void btAddtoTheCart_Click(object sender, EventArgs e)
        {
            if (ChecktheOrder())
            {
                MessageBox.Show("You Can Not Order Nothing!", "Order Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult Result = MessageBox.Show("Are You Sure", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if(Result == DialogResult.OK)
            {
                BurgerOrder OrderSummary = new BurgerOrder();
                OrderSummary.Name = Berger.Type;
                OrderSummary.Size = Berger.Size;
                OrderSummary.Toppings = Berger.Toppings;
                OrderSummary.Quantity = Qn.Value;
                OrderSummary.Price = CalculateTotalPrice();
                _maininterFace.OrderBill(OrderSummary);
                MessageBox.Show("Your Order Added Successfully","Order Done",MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.Close();
            }

            
        }

        private void btAddtoTheCart_MouseEnter(object sender, EventArgs e)
        {
            btAddtoTheCart.ForeColor = Color.White;  
        }

        private void btAddtoTheCart_MouseLeave(object sender, EventArgs e)
        {
            btAddtoTheCart.ForeColor = Color.DarkOrange;
        }

        private void btReset_MouseEnter(object sender, EventArgs e)
        {
            btReset.ForeColor = Color.White;
        }

        private void btReset_MouseLeave(object sender, EventArgs e)
        {
            btReset.ForeColor = Color.DarkOrange;
        }

        private void laReturntoMenue_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void laReturntoMenue_MouseEnter(object sender, EventArgs e)
        {
            laReturntoMenue.ForeColor = Color.White;
        }

        private void laReturntoMenue_MouseLeave(object sender, EventArgs e)
        {
            laReturntoMenue.ForeColor = Color.Red;
        }
    }
    class BurgerOrder : OrderItem
    {
        public string Toppings { get; set; }

        public override string GetOrderSummary()
        {
            return string.Format("Name: {0} Berger\nSize:{1}\nToppings: {2}\nQuantity: {3}", Name, Size, Toppings, Quantity);
        }
    }
}
