using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App2
{
    public partial class MainPage : ContentPage
    {
        public List<Product> Products { get; set; }
        public TableView Table;
        public TableView AddTable;
        public Button BackButton;
        public Button AddButton;
        public MainPage()
        {
            InitializeComponent();
            Products = new List<Product>()
            {
                new Product("Крем для рук", "Faberlic", 150, new DateTime(2022, 12, 25)),
                new Product("Маска для лица", "Oriflame", 220, new DateTime(2023, 6, 15)),
                new Product("Помада", "Gucci", 1000, new DateTime(2023, 2, 4))
            };
            this.BindingContext = this;
        }

        private void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var Product = Products.ElementAt(picker.SelectedIndex);

            if(Table != null && layout.Children.Contains(Table))
            {
                layout.Children.Remove(BackButton);
                layout.Children.Remove(Table);
            }

            Table = new TableView()
            {
                Intent = TableIntent.Form,
                Root = new TableRoot("Ввод данных")
                {
                    new TableSection("Информация")
                    {
                        new TextCell()
                        {
                            Text = "Название",
                            Detail = Product.Name
                        },
                        new TextCell()
                        {
                            Text = "Производитель",
                            Detail = Product.Producer
                        },
                        new TextCell()
                        {
                            Text = "Стоимость",
                            Detail = Product.Price.ToString()
                        },
                        new TextCell()
                        {
                            Text = "Срок годности",
                            Detail = Product.ExpirationDate.ToShortDateString()
                        }
                    }
                }
            };

            layout.Children.Add(Table, 
                Constraint.RelativeToView(picker, (parent, view) =>
                {
                    return picker.X;
                }),
                Constraint.RelativeToView(picker, (parent, view) =>
                {
                    return picker.Y+50;
                }),
                Constraint.Constant(250)
            );

            AddBackButton();
        }

        private void AddBackButton()
        {
            BackButton = new Button()
            {
                Text = "Назад"
            };
            BackButton.Clicked += BackButton_Clicked;

            layout.Children.Add(BackButton,
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.5 - 125;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 1 - 100;
                }),
                Constraint.Constant(250)
            );
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            
            layout.Children.Remove(BackButton);
            if (AddButton != null && layout.Children.Contains(AddButton))
            {
                layout.Children.Remove(AddButton);
            }
            if (Table != null && layout.Children.Contains(Table))
            {
                layout.Children.Remove(Table);
            }
            if (AddTable != null && layout.Children.Contains(AddTable))
            {
                layout.Children.Remove(AddTable);
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            AddTable = new TableView()
            {
                Intent = TableIntent.Form,
                Root = new TableRoot("Ввод данных")
                {
                    new TableSection("Информация")
                    {
                        new EntryCell()
                        {
                            Label = "Название",
                            Placeholder = "Введите название",
                            Keyboard = Keyboard.Plain
                        },
                        new EntryCell()
                        {
                            Label = "Производитель",
                            Placeholder = "Введите производителя",
                            Keyboard = Keyboard.Plain
                        },
                        new EntryCell()
                        {
                            Label = "Стоимость",
                            Placeholder = "Введите цену",
                            Keyboard = Keyboard.Numeric
                        },
                        new EntryCell()
                        {
                            Label = "Срок годности",
                            Placeholder = "Введите дату",
                            Keyboard = Keyboard.Plain,
                        },
                    }
                }
            };

            AddButton = new Button()
            {
                Text = "Сохранить"
            };
            AddButton.Clicked += AddButton_Clicked;

            layout.Children.Add(AddTable,
                Constraint.RelativeToView(picker, (parent, view) =>
                {
                    return picker.X;
                }),
                Constraint.RelativeToView(picker, (parent, view) =>
                {
                    return picker.Y + 50;
                }),
                Constraint.Constant(250)
            );

            layout.Children.Add(AddButton,
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.5 - 125;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 1 - 150;
                }),
                Constraint.Constant(250)
            );

            AddBackButton();
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Подтвердить действие", "Вы хотите сохранить?", "Да", "Нет");
            layout.Children.Remove(AddButton);
            layout.Children.Remove(BackButton);
            layout.Children.Remove(AddTable);
        }
    }
}
