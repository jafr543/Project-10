using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Pizza_Project
{

    public partial class PizzaForm : Form
    {
        private Maininterface _Maininterface;
        public PizzaForm(Maininterface MainInterface)
        {
            InitializeComponent();
          _Maininterface = MainInterface;
        }
        
        int CalculatePizzaTypePrice()
        {
            if(rBBeef.Checked)
             return Convert.ToInt16(rBBeef.Tag);
            
            if (rBCh.Checked)
                return Convert.ToInt16(rBCh.Tag);

            if(rBVe.Checked)
                return Convert.ToInt16(rBVe.Tag);

            if(rBPe.Checked)
                return Convert.ToInt16(rBPe.Tag);

            if(rBMa.Checked)
                return Convert.ToInt16(rBMa.Tag);

            return 0;
        }

        int CalculateSizePrice()
        {
            
            if (rBSmall.Checked)
            {
                return Convert.ToInt16(rBSmall.Tag);
            }

            if(rBMedium.Checked)
            {
                return Convert.ToInt16(rBMedium.Tag);
            }

            if(rBLarge.Checked)
            {
                return Convert.ToInt16(rBLarge.Tag);
            }

            return 0;
        }

        int CalculateCrustPrice()
        { 

            if (rBThin.Checked)
            {
                return 0;
            }

            if(rBThick.Checked)
            {
                return Convert.ToInt16(rBThick.Tag);
            }

            return 0;
        }

        int CalculateToppingsPrice()
        {
            int Price = 0;
            if (cBEx.Checked)
            {
                Price += Convert.ToInt16(cBEx.Tag);
            }

            if (cBMu.Checked)
            {
                Price += Convert.ToInt16(cBMu.Tag);
            }

            if (cBTo.Checked)
            {
                Price += Convert.ToInt16(cBTo.Tag);
            }

            if (cBOn.Checked)
            {
                Price += Convert.ToInt16(cBOn.Tag);
            }

            if (cBOl.Checked)
            {
                Price += Convert.ToInt16(cBOl.Tag);
            }

            if (cBGr.Checked)
            {
                Price += Convert.ToInt16(cBGr.Tag);
            }

            return Price;
        }

        int CalculateTotalPrice()
        {
           return CalculateSizePrice() + CalculateCrustPrice() + CalculateToppingsPrice() + CalculatePizzaTypePrice();
        }

        void PrintTotalPrice()
        {
            laTotalPrice.Text = "$" + CalculateTotalPrice();
        }

        void SelectedPizzaType()
        {
            if (rBBeef.Checked)
            {
             laPizzaType.Text = rBBeef.Text;
                return;
            }
              

            if (rBCh.Checked)
            {
             laPizzaType.Text = rBCh.Text;
            return;
            }
                
            if (rBVe.Checked)
            {
               laPizzaType.Text = rBVe.Text;
               return;
            }
                

            if (rBPe.Checked)
            {
                laPizzaType.Text =  rBPe.Text;
                return;
            }
                

            if (rBMa.Checked)
            {
                laPizzaType.Text =  rBMa.Text;
                return;
            }
                
        }

        void SelectedSize()
        {
            

            if (rBSmall.Checked)
            {
                laSelectedSize.Text = rBSmall.Text;
                return;
            }

            if(rBMedium.Checked)
            {
                laSelectedSize.Text = rBMedium.Text;
                return;
            }

            if(rBLarge.Checked)
            {
                laSelectedSize.Text = rBLarge.Text;
                return;
            }
        }

        void SelectedCrust()
        {

            if (rBThin.Checked)
            {
                laSelectedCrust.Text = rBThin.Text;
                return;
            }
            if(rBThick.Checked) 
            {
                laSelectedCrust.Text = rBThick.Text;
                return;
            }
        }

        void SelectedToppings()
        {
            string sToppings = "";
            
            if(cBEx.Checked)
            {
                sToppings = cBEx.Text + " ,";
            }

            if (cBMu.Checked)
            {
                sToppings += cBMu.Text + " ,";
            }

            if (cBTo.Checked)
            {
                sToppings += cBTo.Text + " ,";
            }

            if (cBOn.Checked)
            {
                sToppings += cBOn.Text + " ,";
            }

            if (cBOl.Checked)
            {
                sToppings += cBOl.Text + " ,";
            }

            if (cBGr.Checked)
            {
                sToppings += cBGr.Text + " ,";
            }

            laSelectedToppings.Text = sToppings.TrimEnd(',',' ');
        }

        void TurnonButtenes()
        {
            plSize.Enabled = true;
            plCurst.Enabled = true;
            plToppings.Enabled = true;
            btOrder.Enabled = true;
            plPizzaType.Enabled = true;

            cBEx.Checked = false;
            cBMu.Checked = false;
            cBTo.Checked = false;
            cBOn.Checked = false;
            cBOl.Checked = false;
            cBGr.Checked = false;

            rBSmall.Checked = true;
            rBThin.Checked = true;
            rBBeef.Checked = true;

            laSelectedToppings.Text = "no Toppings";

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

        private void Form1_Load(object sender, EventArgs e)
        {
            SelectedPizzaType();
            SelectedSize();
            SelectedCrust();
            PrintTotalPrice();
        }

        private void CheckBoxes_CheckedChanged(object sender, EventArgs e)
        {
            SelectedToppings();
            PrintTotalPrice();
        }

        private void rBSize_CheckedChanged(object sender, EventArgs e)
        {
            SelectedSize();
            PrintTotalPrice();
        }

        private void CrustTypeChanged(object sender, EventArgs e)
        {
            SelectedCrust();
            PrintTotalPrice();
        }

        private void btOrder_Click(object sender, EventArgs e)
        {
            if (ChecktheOrder())
            {
                MessageBox.Show("You Can Not Order Nothing!", "Order Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult Result = MessageBox.Show("Are you Sure?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (Result == DialogResult.OK)
            {
                PizzaOrder OrderSummary = new PizzaOrder();
                OrderSummary.Name = laPizzaType.Text;
                OrderSummary.Size = laSelectedSize.Text;
                OrderSummary.Crust = laSelectedCrust.Text + " Crust" ;
                OrderSummary.Toppings = laSelectedToppings.Text;
                OrderSummary.Price = CalculateTotalPrice();
                _Maininterface.OrderBill(OrderSummary);
                MessageBox.Show("Order Added Successfully", "Order Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void bReset_Click(object sender, EventArgs e)
        {
            TurnonButtenes();
        }

        private void TypeChenge(object sender, EventArgs e)
        {
            SelectedPizzaType();
            PrintTotalPrice();
        }

        private void BtCloseMenue_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtCloseMenue_MouseEnter(object sender, EventArgs e)
        {
            BtCloseMenue.ForeColor = Color.White;
        }

        private void BtCloseMenue_MouseLeave(object sender, EventArgs e)
        {
            BtCloseMenue.ForeColor = Color.Red;
        }

        private void bOrder_MouseEnter(object sender, EventArgs e)
        {
            btOrder.ForeColor = Color.White;
        }

        private void btOrder_MouseLeave(object sender, EventArgs e)
        {
            btOrder.ForeColor = Color.LightSeaGreen;
        }

        private void btReset_MouseEnter(object sender, EventArgs e)
        {
            btReset.ForeColor = Color.White;
        }

        private void btReset_MouseLeave(object sender, EventArgs e)
        {
            btReset.ForeColor = Color.LightSeaGreen;
        }
    }
    class PizzaOrder : OrderItem
    {
        public string Crust { get; set; }
        public string Toppings { get; set; }

        public override string GetOrderSummary()
        {
            return string.Format("Name: {0}\nSize: {1}\nCrust Type: {2}\nToppings: {3}", Name, Size, Crust, Toppings);
        }

    }
}
