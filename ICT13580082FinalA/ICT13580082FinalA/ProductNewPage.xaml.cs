using ICT13580082FinalA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ICT13580082FinalA
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductNewPage : ContentPage
    {
        Product product;

        public ProductNewPage(Product product = null)
        {
            InitializeComponent();

            this.product = product;

            titleLabel.Text = product == null ? "เพิ่มสินค้า" : "แก้ไขข้อมูลสินค้า";

            saveButton.Clicked += SaveButton_Clicked;
            cancelButton.Clicked += CancelButton_Clicked;


            genderPicker.Items.Add("ชาย");
            genderPicker.Items.Add("หญิง");
           
            departmentPicker.Items.Add("ฝ่ายการเงิน");
            departmentPicker.Items.Add("ฝ่ายบุคคล");
            departmentPicker.Items.Add("ฝ่ายการตลาด");
            departmentPicker.Items.Add("ฝ่ายริหาร");

            statusPicker.Items.Add("โสด");
            statusPicker.Items.Add("สมรส");
            statusPicker.Items.Add("หย่าร้าง");
   
            salarySlider.ValueChanged += SalarySlider_ValueChanged;

            childStepper.ValueChanged += ChildStepper_ValueChanged;

            if (product != null)
            {
                nameEntry.Text = product.Name;
                lastNameEntry.Text = product.LastName;
                ageEntry.Text = product.Age.ToString();
                genderPicker.SelectedItem = product.Gender;
                departmentPicker.SelectedItem = product.Department;
                numberEntry.Text = product.PhoneNumber;
                emailEntry.Text = product.Email;
                addressEditor.Text = product.Address;
                statusPicker.SelectedItem = product.Status;
                valueLabel.Text = product.Child;
                value2Label.Text = product.Salary.ToString();
       
            }

        }

        private void ChildStepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            int value = (int)e.NewValue;
            valueLabel.Text = value.ToString();
        }

        private void SalarySlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            int value = (int)e.NewValue;
            value2Label.Text = value.ToString();
        }

        async void SaveButton_Clicked(object sender, EventArgs e)
        {
            var isOk = await DisplayAlert("ยืนยัน", "คุณต้องการบันทึกใช่หรือไม่", "ใช่", "ไม่ใช่");

            if (isOk)
            {
                if (product == null)
                {
                    product = new Product();
                    product.Name = nameEntry.Text;
                    product.LastName = lastNameEntry.Text;
                    product.Age = int.Parse(ageEntry.Text);
                    product.Gender = genderPicker.SelectedItem.ToString();
                   
                    product.Department = departmentPicker.SelectedItem.ToString();
                    product.PhoneNumber = numberEntry.Text;
                    product.Email = emailEntry.Text;
                    product.Address = addressEditor.Text;
                    product.Status = statusPicker.SelectedItem.ToString();
                    product.Child = valueLabel.Text;
                    product.Salary = decimal.Parse(value2Label.Text);
                   
                    
                    var id = App.DbHelper.AddProduct(product);
                    await DisplayAlert("บันทึกสำเร็จ", "รหัสสินค้าของท่านคือ#" + id, "ตกลง");
                }
                else
                {
                    product = new Product();
                    product.Name = nameEntry.Text;
                    product.LastName = lastNameEntry.Text;
                    product.Age = int.Parse(ageEntry.Text);
                    product.Gender = genderPicker.SelectedItem.ToString();

                    product.Department = departmentPicker.SelectedItem.ToString();
                    product.PhoneNumber = numberEntry.Text;
                    product.Email = emailEntry.Text;
                    product.Address = addressEditor.Text;
                    product.Status = statusPicker.SelectedItem.ToString();
                    product.Child = valueLabel.Text;
                    product.Salary = decimal.Parse(value2Label.Text);
                    
                    var id = App.DbHelper.UpdateProduct(product);
                    await DisplayAlert("บันทึกสำเร็จ", "แก้ไขข้อมูลสินค้าเรียบร้อย" + id, "ตกลง");
                }
                await Navigation.PopModalAsync();
            }
        }

        void CancelButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}