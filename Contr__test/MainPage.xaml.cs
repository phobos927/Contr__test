using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SQLite.Net;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using System.Threading.Tasks;
using System.Text;
using Windows.UI.Popups;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace Contr__test
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        SQLiteConnection con;
        string path;
        SQLiteCommand com;
        int sel_it, sel_id;
        byte[] byte_img_test;
        byte[] array_open;
        bool c_img = false, c_nm = false, c_ad = false, c_ph = false, c_em = false, c_si = false, c_in = false;


        public MainPage()
        {
            this.InitializeComponent();
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db_c.sqlite");
            con = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            con.CreateTable<class_db>();
            // com = con.CreateCommand("SELECT * FROM class_db ");



        }

        private void show()
        {
            var show = con.Table<class_db>();


            id_list.Items.Clear();
            img_list.Items.Clear();
            name_list.Items.Clear();
            adress_list.Items.Clear();
            phone_list.Items.Clear();
            email_list.Items.Clear();
            site_list.Items.Clear();
            info_list.Items.Clear();

            foreach (var item in show)
            {
                id_list.Items.Add(item.Id);
                img_list.Items.Add(item.img_);
                name_list.Items.Add(item.name_);
                adress_list.Items.Add(item.adress_);
                phone_list.Items.Add(item.phone_);
                email_list.Items.Add(item.email_);
                site_list.Items.Add(item.site_);
                info_list.Items.Add(item.info_);
            }


        }

        private async void add_Click(object sender, RoutedEventArgs e)
        {
            if (c_img && c_nm && c_ad && c_ph && c_em && c_si && c_in)
            {
                var add = con.Insert(new class_db
                {
                    img_ = byte_img_test,
                    name_ = name.Text,
                    adress_ = adress.Text,
                    phone_ = phone.Text,
                    email_ = email.Text,
                    site_ = site.Text,
                    info_ = info.Text

                });
                show();
            }
            else
            {
                var dialog = new MessageDialog("Все поля обязательны!");
                await dialog.ShowAsync();
            }
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            var del = con.Delete<class_db>(sel_id);
            show();
        }


        private void id_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sel_it = id_list.SelectedIndex;
            img_list.SelectedIndex = sel_it;
            name_list.SelectedIndex = sel_it;
            adress_list.SelectedIndex = sel_it;
            phone_list.SelectedIndex = sel_it;
            email_list.SelectedIndex = sel_it;
            site_list.SelectedIndex = sel_it;
            info_list.SelectedIndex = sel_it;
            id_list.SelectedIndex = sel_it;
            sel_id = Convert.ToInt32(id_list.SelectedItem);

        }

        private void img_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sel_it = img_list.SelectedIndex;
            img_list.SelectedIndex = sel_it;
            name_list.SelectedIndex = sel_it;
            adress_list.SelectedIndex = sel_it;
            phone_list.SelectedIndex = sel_it;
            email_list.SelectedIndex = sel_it;
            site_list.SelectedIndex = sel_it;
            info_list.SelectedIndex = sel_it;
            id_list.SelectedIndex = sel_it;

            read_byteAsync();




        }


        public async void read_byteAsync()
        {
            int i = 0;

            var show = con.Table<class_db>();
            foreach (var item in show)
            {
                if (i == sel_it)
                {
                    array_open = item.img_;
                }
                i++;

            }


            using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
            {
                using (DataWriter writer = new DataWriter(stream.GetOutputStreamAt(0)))
                {
                    writer.WriteBytes(this.array_open);
                    await writer.StoreAsync();
                }
                var image = new BitmapImage();
                await image.SetSourceAsync(stream);
                image2.Source = image;

            }
        }

        private void name_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sel_it = name_list.SelectedIndex;
            img_list.SelectedIndex = sel_it;
            name_list.SelectedIndex = sel_it;
            adress_list.SelectedIndex = sel_it;
            phone_list.SelectedIndex = sel_it;
            email_list.SelectedIndex = sel_it;
            site_list.SelectedIndex = sel_it;
            info_list.SelectedIndex = sel_it;
            id_list.SelectedIndex = sel_it;
        }

        private void adress_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sel_it = adress_list.SelectedIndex;
            img_list.SelectedIndex = sel_it;
            name_list.SelectedIndex = sel_it;
            adress_list.SelectedIndex = sel_it;
            phone_list.SelectedIndex = sel_it;
            email_list.SelectedIndex = sel_it;
            site_list.SelectedIndex = sel_it;
            info_list.SelectedIndex = sel_it;
            id_list.SelectedIndex = sel_it;
        }

        private void name_TextChanged(object sender, TextChangedEventArgs e)
        {
            c_nm = V_X(name, V_nm, c_nm);
        }

        private bool V_X(TextBox text, Image img, bool ch)
        {
            if (text.Text != null)
            {
                ch = true;
                img.Source = V_ok.Source;
                return ch;

            }
            else
            {
                ch = false;
                img.Source = V_no.Source;
                return ch;
            }

        }

        private void adress_TextChanged(object sender, TextChangedEventArgs e)
        {
            c_ad = V_X(adress, V_adr, c_ad);
        }

        private void phone_TextChanged(object sender, TextChangedEventArgs e)
        {
            c_ph = V_X(phone, V_ph, c_ph);
        }

        private void email_TextChanged(object sender, TextChangedEventArgs e)
        {
            c_em = V_X(email, V_em, c_em);
        }

        private void site_TextChanged(object sender, TextChangedEventArgs e)
        {
            c_si = V_X(site, V_Si, c_si);
        }

        private void info_TextChanged(object sender, TextChangedEventArgs e)
        {
            c_in = V_X(info, V_inf, c_in);
        }

        private void phone_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sel_it = phone_list.SelectedIndex;
            img_list.SelectedIndex = sel_it;
            name_list.SelectedIndex = sel_it;
            adress_list.SelectedIndex = sel_it;
            phone_list.SelectedIndex = sel_it;
            email_list.SelectedIndex = sel_it;
            site_list.SelectedIndex = sel_it;
            info_list.SelectedIndex = sel_it;
            id_list.SelectedIndex = sel_it;
        }

        private void email_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sel_it = email_list.SelectedIndex;
            img_list.SelectedIndex = sel_it;
            name_list.SelectedIndex = sel_it;
            adress_list.SelectedIndex = sel_it;
            phone_list.SelectedIndex = sel_it;
            email_list.SelectedIndex = sel_it;
            site_list.SelectedIndex = sel_it;
            info_list.SelectedIndex = sel_it;
            id_list.SelectedIndex = sel_it;
        }

        private void site_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sel_it = site_list.SelectedIndex;
            img_list.SelectedIndex = sel_it;
            name_list.SelectedIndex = sel_it;
            adress_list.SelectedIndex = sel_it;
            phone_list.SelectedIndex = sel_it;
            email_list.SelectedIndex = sel_it;
            site_list.SelectedIndex = sel_it;
            info_list.SelectedIndex = sel_it;
            id_list.SelectedIndex = sel_it;
        }

        private async System.Threading.Tasks.Task ins_img_ClickAsync()
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation =
                Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {


                using (IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
                {

                    BitmapImage bitmapImage = new BitmapImage();

                    await bitmapImage.SetSourceAsync(fileStream);
                    image2.Source = bitmapImage;

                }

                using (var inputStream = await file.OpenSequentialReadAsync())
                {
                    var readStream = inputStream.AsStreamForRead();

                    var byteArray = new byte[readStream.Length];
                    await readStream.ReadAsync(byteArray, 0, byteArray.Length);
                    byte_img_test = byteArray;
                }


            }
            else
            {

            }

        }





        private async void ins_img_ClickAsync(object sender, RoutedEventArgs e)
        {
            try
            {
                await ins_img_ClickAsync();
            }
            catch (System.NullReferenceException)
            {
            }
            finally
            {
                c_img = true;

                V_img.Source = V_ok.Source;

            }



        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            show();
        }

        private void info_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sel_it = info_list.SelectedIndex;
            img_list.SelectedIndex = sel_it;
            name_list.SelectedIndex = sel_it;
            adress_list.SelectedIndex = sel_it;
            phone_list.SelectedIndex = sel_it;
            email_list.SelectedIndex = sel_it;
            site_list.SelectedIndex = sel_it;
            info_list.SelectedIndex = sel_it;
            id_list.SelectedIndex = sel_it;
        }


    }
}