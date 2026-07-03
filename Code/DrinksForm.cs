using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pizza_Project
{ 

    public partial class DrinksForm : Form
    {
        Maininterface _Maininterface;
        public DrinksForm(Maininterface MainInterface)
        {
            InitializeComponent();
            _Maininterface = MainInterface;
        }
        
        private struct DrinksandSides
        {
           public string Drinks;
           public string Size;
           public string Sides;
        }

        DrinksandSides Drinks_and_Sides = new DrinksandSides();

        int CalculateDrinksPrice()
        {
            string Driks = string.Empty;
            int drinksCount = 0;
            int drinksPrice = 0;

            if(chCo.Checked)
            {
                Driks += chCo.Text + ", ";
                drinksPrice+= Convert.ToInt16(chCo.Tag);
                drinksCount++;
            }

            if(chDC.Checked)
            {
                Driks += chDC.Text + ", ";
                drinksPrice+= Convert.ToInt16(chDC.Tag);
                drinksCount++;
            }

            if(chOJ.Checked)
            {
                Driks += chOJ.Text + ", ";
                drinksPrice += Convert.ToInt16(chOJ.Tag);
                drinksCount++;
            }

            if(chWt.Checked)
            {
                Driks += chWt.Text + ", ";
                drinksPrice += Convert.ToInt16(chWt.Tag);
                drinksCount++;
            }

            Drinks_and_Sides.Drinks = Driks.TrimEnd(',',' ');

            if (rdSm.Checked)
            {
                Drinks_and_Sides.Size = rdSm.Text;
                return drinksPrice += (Convert.ToInt16(rdSm.Tag) * drinksCount);
            }

            if (rdMe.Checked)
            {
                Drinks_and_Sides.Size = rdMe.Text;
                return drinksPrice += (Convert.ToInt16(rdMe.Tag) * drinksCount);
            }

            if (rdLg.Checked)
            {
                Drinks_and_Sides.Size = rdLg.Text;
                return drinksPrice += (Convert.ToInt16(rdLg.Tag) * drinksCount);
            }

            return drinksPrice;
        }

        int CalculateSidesPrice()
        {
            string Sides = string.Empty;
            int sidesPrice = 0;

            if(chFF.Checked)
            {
                Sides += chFF.Text + ", ";
                sidesPrice+= Convert.ToInt16(chFF.Tag);
            }

            if(chOR.Checked)
            {
                Sides += chOR.Text + ", ";
                sidesPrice+= Convert.ToInt16(chOR.Tag);
            }

            if(chMS.Checked)
            {
                Sides += chMS.Text + ", ";
                sidesPrice+= Convert.ToInt16(chMS.Tag);
            }

            if(chGB.Checked)
            {
                Sides += chGB.Text + ", ";
                sidesPrice+= Convert.ToInt16(chGB.Tag);
            }
            Drinks_and_Sides.Sides = Sides.TrimEnd(',', ' ');
            return sidesPrice;
        }
        
        int CalculateTotalPrice()
        {
            return (Convert.ToInt16(NUDdrinks.Value) * CalculateDrinksPrice()) + (Convert.ToInt16(NUDsides.Value) * CalculateSidesPrice());
        }

        void PrintTotalPrice()
        {
            laPrice.Text = "$" + CalculateTotalPrice();
        }

        void ResetForm()
        {
            chCo.Checked = true;
            chDC.Checked = false;
            chOJ.Checked = false;
            chWt.Checked = false;

            rdSm.Checked = true;

            chFF.Checked = true;
            chOR.Checked = false;
            chMS.Checked = false;
            chGB.Checked = false;
            NUDdrinks.Value = 1;
            NUDsides.Value = 1;
        }

        bool ChecktheOrder()
        {
            if(CalculateTotalPrice() == 0)
            {
                return true;
            }

            return false;
        }
        private void DrinksCheckChanged(object sender, EventArgs e)
        {
            PrintTotalPrice();
        }

        private void DrinkSizeCheckedChanged(object sender, EventArgs e)
        {
            PrintTotalPrice();
        }

        private void SidesCheckedChanged(object sender, EventArgs e)
        {
            PrintTotalPrice();
        }

        private void Drinks_Sides_NUD_ValueChanged(object sender, EventArgs e)
        {
            PrintTotalPrice();
        }

        private void btReset_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void btAddtoCart_Click(object sender, EventArgs e)
        {
           if(ChecktheOrder())
           {
                    MessageBox.Show("You Can Not Order Nothing!", "Order Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
           }

            DialogResult Result = MessageBox.Show("Are you Sure?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if(Result == DialogResult.OK)
            {

                Drinks_SidesOrder OrderSummary = new Drinks_SidesOrder();
                OrderSummary.Name = Drinks_and_Sides.Drinks;
                OrderSummary.Size = Drinks_and_Sides.Size;
                OrderSummary.Quantity = NUDdrinks.Value;
                OrderSummary.SidesNames = Drinks_and_Sides.Sides;
                OrderSummary.SidesQuantity = NUDsides.Value;
                OrderSummary.Price = CalculateTotalPrice();
                _Maininterface.OrderBill(OrderSummary);
                MessageBox.Show("Order Added Successfully", "Order Done",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                this.Close();
            }
        }

        private void DrinksForm_Load(object sender, EventArgs e)
        {
            ResetForm();
            PrintTotalPrice();
        }

        private void btReset_MouseEnter(object sender, EventArgs e)
        {
            btReset.ForeColor = Color.White;
        }

        private void btReset_MouseLeave(object sender, EventArgs e)
        {
            btReset.ForeColor = Color.MediumSlateBlue;
        }

        private void btAddtoCart_MouseEnter(object sender, EventArgs e)
        {
            btAddtoCart.ForeColor = Color.White;
        }

        private void btAddtoCart_MouseLeave(object sender, EventArgs e)
        {
            btAddtoCart.ForeColor = Color.MediumSlateBlue;
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
    class Drinks_SidesOrder : OrderItem
    {
        public string SidesNames { get; set; }
        public decimal SidesQuantity { get; set; }

        public override string GetOrderSummary()
        {
            return string.Format("Drink(s): {0}\nDrinkSize: {1}\nDrinkQuantity: {2}\nSide(s): {3}\nSideQuantity: {4}"
                , Name, Size, Quantity, SidesNames, SidesQuantity);
        }
    }
}
