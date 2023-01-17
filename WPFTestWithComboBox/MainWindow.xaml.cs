using System;
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

namespace WPFTestWithComboBox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string[] operate { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            operate = new string[] { "Please select", "Test Scores", "Bank Charges", "Shipping Charge", "Speed" };
            DataContext = this;
        }

        private void process_Click(object sender, RoutedEventArgs e)
        {
            if (operation.SelectedIndex == 1){
                testScores();
            }
            else if ((String)(operation.SelectedValue) == "Bank Charges"){
                bankCharge();
            }
            else if (operation.SelectedIndex == 3){
                shippingCharges();
            }
            else if ((String)(operation.SelectedValue) == "Speed"){
                distanceTraveled();
            }
        }

        private void testScores()
        {
            //Receive the inputs
            double a1 = Convert.ToDouble(score1.Text);
            double a2 = Convert.ToDouble(score2.Text);
            double a3 = Convert.ToDouble(score3.Text);
            double ave = (a1 + a2 + a3) / 3;

            //validate the inputs
            if ((a1 < 0 || a1 > 100) || (a2 < 0 || a2 > 100) || (a3 < 0 || a3 > 100))
            {
                averageScore.Text = "Invalid input(s)";
                gradeScore.Text = "Cannot get grade";
            }
            else if (ave >= 90 && ave <= 100)
            {
                averageScore.Text = ave.ToString();
                gradeScore.Text = "A";
            }
            else if (ave >= 80 && ave <= 89)
            {
                averageScore.Text = ave.ToString();
                gradeScore.Text = "B";
            }
            else if (ave >= 70 && ave <= 79)
            {
                averageScore.Text = ave.ToString();
                gradeScore.Text = "C";
            }
            else if (ave >= 60 && ave <= 69)
            {
                averageScore.Text = ave.ToString();
                gradeScore.Text = "D";
            }
            else if (ave < 60)
            {
                averageScore.Text = ave.ToString();
                gradeScore.Text = "F";
            }
            else
            {
                gradeScore.Text = "Error";
            }
        }

        private void bankCharge()
        {
            double balance = 0;// Initialize the variables 
            double newBalance = 0;
            int checks = 0;
            int newChecks = 0;
            int newMonthChecks = 0;
            double serviceFee = 0;

            balance = Convert.ToDouble(openingBalance.Text);//Receive the opening balance from input

            newMonthChecks = Convert.ToInt16(checksUsed.Text);//Received the checks used for the month from input                            
            if (totalChecksUsed.Text == "")
            {//validate previous used checks
                checks = 0;
            }
            else
            {
                checks = Convert.ToInt16(totalChecksUsed.Text);
            }
            newChecks = countChecks(checks, newMonthChecks);// Get total checks used
            totalChecksUsed.Text = newChecks.ToString();

            serviceFee = getFee(newMonthChecks, balance); // Get service fee for the month
            serviceFeeForMonth.Text = serviceFee.ToString();

            newBalance = getBalance(balance, newMonthChecks); //Get ending balance
            endingBalance.Text = newBalance.ToString();
            balance = newBalance; // Send the ending balance to the opening balance for next cycle
            openingBalance.Text = balance.ToString();

        }

        private static int countChecks(int preChecks, int newChecks) // To calculate the checks
        {
            int checks = 0;
            checks = preChecks + newChecks;
            return checks;
        }

        private static double getBalance(double prebal, int checks) //To check the month-end balance
        {
            double bal = prebal - checkCost(checks);
            if (bal < 400)
            {
                bal = bal - 15;
            }
            return bal;
        }
        private static double checkCost(int checks) //To calculate the ckeck cost
        {
            double fee = 10;
            if (checks >= 0 && checks <= 19)
            {
                fee = fee + checks * 0.1;
            }
            else if (checks >= 20 && checks <= 39)
            {
                fee = fee + checks * 0.08;
            }
            else if (checks >= 40 && checks <= 59)
            {
                fee = fee + checks * 0.06;
            }
            else if (checks >= 60)
            {
                fee = fee + checks * 0.04;
            }
            else
            {
                Console.WriteLine("Invalid input");
            }
            return fee;
        }
        private static double getFee(int checks, double balance) // To calculate the monthly fee
        {
            double fee = 0;
            double ckCost = checkCost(checks);
            if (balance - ckCost >= 400)
            {
                fee = ckCost;
            }
            else
            {
                fee = ckCost + 15;
            }
            return fee;
        }
        private void shippingCharges()
        {
            int weight = Convert.ToInt16(kg.Text); //Receive weight and distance from inputs
            int distance = Convert.ToInt16(miles.Text);

            shippingFee.Text = getCharge(weight, distance).ToString();
        }
        private static double getCharge(double weight, double distance)
        {
            int distanceBracket = (int)(distance / 500) + 1; //0~499 is bracket1, 500~999 is bracket2
            double charge = 0;
            if (weight > 0 && weight <= 2) // Calculate the shipping charge according the weight and distance bracket
            {
                charge = distanceBracket * 1.1;
            }
            else if (weight > 2 && weight <= 6)
            {
                charge = distanceBracket * 2.2;
            }
            else if (weight > 6 && weight <= 10)
            {
                charge = distanceBracket * 3.7;
            }
            else if (weight > 10)
            {
                charge = distanceBracket * 4.8;
            }
            return charge;
        }
        private void distanceTraveled()
        {
            int s = Convert.ToUInt16(speed.Text);

            int[] arrDistance = getDistance(s, 3);
            hour1.Text = arrDistance[0].ToString();
            hour2.Text = arrDistance[1].ToString();
            hour3.Text = arrDistance[2].ToString();

        }
        private static int[] getDistance(int speed, int numberOfHours)
        {
            int[] arrDistance = new int[numberOfHours];
            for (int i = 0; i < numberOfHours; i++)
            {
                arrDistance[i] = speed * (i + 1);
            }
            return arrDistance;
        }
    }
}
