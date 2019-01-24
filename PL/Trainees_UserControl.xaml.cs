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
using BL;
using BE;
using System.Xml.Linq;

namespace PL
{
    /// <summary>
    /// Interaction logic for Trainees_UserControl.xaml
    /// </summary>
    public partial class Trainees_UserControl : UserControl
    {

        private IBL bl = FactoryBL.GetBL(Configuration.BLType);
        private List<Trainee> allTraineeList;
        private List<Trainee> currentUseList;
        Predicate<Trainee> carTypeFilter=delegate { return true; };
        Predicate<Trainee> truckFilter = delegate { return true; };
        Predicate<Trainee> smallTruckFilter = delegate { return true; };
        Predicate<Trainee> privetCarFilter = delegate { return true; };
        Predicate<Trainee> motorcycleFilter = delegate { return true; };

        public Trainees_UserControl()
        {
            InitializeComponent();
            bl.TraineeEvent += TraineeEvent;
            string[] SortByValues = { "firstName", "lastName", "id", "number of lesson", "gender", "age", "school name", "car type", "teacher name" };
            allTraineeList = bl.getAllTrainees(); //To prevent from get the all list every simple action, save the whole list until the original list change (in the event this variable will get the new list)
            currentUseList = allTraineeList; //on this variable all the changes will be done
            ComboBoxSortBy.ItemsSource = SortByValues;

            if (FactoryBL.GetBL("myBL").getAllTrainees().Count==0)
            {
                string fName, lName, cityName;
                Random random = new Random(DateTime.Now.Millisecond);
                List<string> schstr = new List<string> { " ארבע על 4", "בני י-ם", "אגד י-ם", "רמזור בע\"מ", "עוז", "על גלגלים", "הבירה", "אור ירוק", "דרכים", "טוטח", "דרייב קול", "רעים", "4 דרייב", "סמארט דריי", "איחוד", "אקספרס", "אביר", "צוק תחבורה", "ראשונים", "משא בטוח", "הצלחה", "מנקודה לנקודה", "במבי", "הלוך ראשוני", "גלגלים", "דן", "או-קיי", "מקוה ישראל", "הנהג", "יחדיו", "נאור", "יוזמה", "איציק-סלע", "יהלום", "גולן", "הנדסה בגובה", "אילון", "איציק השגים", "קרן", "עמירם", "גל און", "קפלר", "אוטו", "נתיב", "מגל", "שלאגר", "נתיב הצפון", "דרכים אלפי ", "המקצוענים" };
                List<string> teastr = new List<string>();
                List<string> lstr = new List<string> { "נאור","כהן", "לוי", "מזרחי", "פרץ", "ביטון", "דהן", "פרידמן", "מלכה", "אזולאי", "כץ", "יוסף", "דוד", "עמר", "אוחיון", "חדד", "גבאי", "בן דוד", "אדרי", "לוין", "טל", "קליין", "חן", "שפירא", "חזן", "משה", "אשכנזי", "אוחנה", "סגל", "גולן", "יצחק", "בר", "מור", "דיין", "אלבז", "בכר", "סויסה", "שמש", "רוזנברג", "לביא", "אטיאס", "נחום", "שרעבי", "שטרן", "ממן", "שחר", "אלון", "שורץ", "ששון", "עובדיה", "גרינברג", "בן חמו", "וקנין", "אסולין", "מימון", "מאיר", "פלדמן", "גולדשטיין", "ברוך", "וייס", "אמסלם", "רובין", "עזרא", "סבג", "גולדברג", "אברמוב", "קדוש", "הראל", "צור", "שוורץ", "רבינוביץ", "אהרוני", "מילר", "קפלן", "שושן", "הרוש", "סולומון", "הרשקוביץ", "רוזן", "ברקוביץ", "גרוס", "נגר", "חיון", "מלול", "סלע", "פלד", "בן שושן", "צרפתי", "אסרף", "שטרית", "גוטמן", "עאמר", "פרנקל", "זוהר", "מויאל", "אלפסי", "ברגר", "פישר" };
                List<string> fnstr = new List<string> { "מלתיאל","אליה", "אליהב", "אליהו", "גבריאל", "גלעד", "גד", "עידן", "מיכאל", "מנשה", "ירון", "ראם", "רז", "רפאל", "רביד", "שהם", "שחר", "שי", "תמיר", "דב", "דביר", "דניאל", "דורון", "דרור", "דולב", "הראל", "הלל", "זיו", "חזקיה", "חנניה", "יאיר", "ירדן", "ישי", "ינון", "ישורון", "ישראל", "יונה", "כפיר", "לביא", "לוי", "מאיר", "מתתיהו", "מרדכי", "נחמן", "עדו", "ערן", "גרשון", "זאב", "אליעזר", "אליקים", "אלישיב", "אלישע", "זבולון", "אלמוג", "יחזקאל", "יהושע", "נריה", "גדעון", "שמואל", "שגיא", "בועז", "שמשון", "ישעיהו", "ירמיה", "עובדיה", "קהת", "נחשון", "משה", "אברהם", "יעקב", "יצחק", "יהונתן", "שאול", "דוד", "שלמה", "יוסף", "יששכר", "יהודה", "לוי", "שמעון", "ראובן", "ציון", "אלנתן", "אלעד", "אלעזר", "אלקנה", "פרץ", "פסח", "צבי", "צדוק", "צפניה", "צפריר", "פנחס", "זכריה", "אלרואי", "אמית", "אמיתי", "אמנון", "אמציה", "אסי", "נאור", "נבו", "נדב", "נהוראי", "נוה", "נעם", "נח", "נחום", "נחמיה", "נחמן", "נחשון", "ניב", "ניסים", "ניסן", "ניצן", "חביב", "חגי", "חובב", "חי", "חיים", "חן", "חנוך", "חנן", "חננאל" };
                List<string> cstr = new List<string> { "אבן יהודה", "אופקים", "אור הנר", "אור יהודה", "אזור", "אילת", "אמירים", "אפרת", "אריאל", "אשדוד", "אשקלון", "באר יעקב", "באר שבע", "בית דגן", "בית שאן", "בית שמש", "בית שערים", "בני ברק", "בנימינה", "בת ים", "גבעת סביון", "גבעת שמואל", "גבעתיים", "גדרה", "גלעד", "גן יבנה", "גני תקוה", "הוד השרון", "הרצליה", "זכרון יעקב", "חדרה", "חולון", "חיפה", "חצור הגלילית", "טבעון", "טבריה", "טירת הכרמל", "יבנה", "יהוד", "יפו", "יקנעם עילית", "ירוחם", "ירושלים", "כוכב יאיר", "כפר אזר", "כפר ורדים", "כפר סבא", "כפר שמריהו", "כפר תבור", "כרכור", "כרמיאל", "לוד", "לפיד", "מבשרת ציון", "מגדל", "מגדל העמק", "מודיעין", "מטולה", "מכבים", "מכמורת", "מפלסים", "מצפה רמון", "מקווה ישראל", "נהריה", "נווה אור", "נווה איתן", "נס ציונה", "נען", "נצרת", "נצרת עילית", "נתניה", "סביון", "עכו", "עפולה", "ערד", "עתלית", "פרדס חנה", "פתח-תקוה", "צור הדסה", "צורעה", "צפת", "קדימה", "קיבוץ אורטל", "קיבוץ נחל עוז", "קיבוץ עלומים", "קיסריה", "קצרין", "קרית אונו", "קרית אתא", "קרית ביאליק", "קרית גת", "קרית מוצקין", "קרית שמונה", "ראש העין", "ראש פינה", "ראשון לציון", "רחובות", "רמלה", "רמת אפעל", "רמת השרון", "רמת ישי", "רמת רזיאל", "רמת-גן", "רעננה", "שדרות", "שוהם", "שילה", "תל-אביב" };
                List<string> x;
                for (int i = 0; i < schstr.Count*3; i++)
                {
                    teastr.Add(lstr[random.Next() % lstr.Count] + " " + fnstr[random.Next() % fnstr.Count]);
                }
                    XElement streetName = XElement.Load(@"dataXML\streetXML.xml");
                for (int i = 0; i < 1000; i++)
                {
                    fName = fnstr[random.Next() % fnstr.Count];
                    lName = lstr[random.Next() % lstr.Count];
                    do
                    {
                        cityName = cstr[random.Next() % cstr.Count];
                        x = (from stre in streetName.Elements()
                             where stre.Element("city_name").Value.Contains(cityName)
                             select stre.Element("street_name").Value).ToList();
                    } while (x.Count == 0);
                    int num = random.Next(1979, 2001);
                    int temp1 = (random.Next() % 2);
                    int temp2 = (random.Next() % 2);
                    int carnum = Math.Max(temp1, temp2);
                    int gearnum = Math.Min(temp1, temp2);
                    bl.addTrainee(new Trainee((i + 99715).ToString("00000000"), fName, lName, new DateTime(num,
                        i % 11 + 1, i % 24 + 1), schstr[i % schstr.Count], teastr[i % schstr.Count*3 + (gearnum+carnum) % 3], i % 15 + 21, "0509024" + i.ToString("000"),
                        new Address(x[random.Next() % x.Count].Trim(), i % 13 + 1, cityName),
                        BE.Gender.MALE,
                        (BE.CarType)carnum, (BE.GearBox)gearnum));
                }
                //    bl.addTrainee(new Trainee("12345670", "yosef", "machanaim", new DateTime(1980, 1, 1), "normal_Gaz_School", "eliyahu", 28, "99999999", new Address("shahal", 7, "jerusalem "), Gender.MALE, CarType.PrivetCar, GearBox.Manual));
                //bl.addTrainee(new Trainee("12345671", "hagai", "sugerman", new DateTime(1980, 1, 1), "normal_Gaz_School", "eliyahu", 25, "99999999", new Address("yehosua", 11, "jerusalem "), Gender.MALE, CarType.PrivetCar, GearBox.Manual));
                //bl.addTrainee(new Trainee("12345672", "moshe", "shauli", new DateTime(1980, 1, 1), "normal_Gaz_School", "eliyahu", 30, "99999999", new Address("shahal", 7, "jerusalem "), Gender.MALE, CarType.PrivetCar, GearBox.Manual));
                //bl.addTrainee(new Trainee("12345673", "david", "bar-hai", new DateTime(1980, 1, 1), "normal_Gaz_School", "eliyahu", 28, "99999999", new Address("shahal", 7, "jerusalem "), Gender.MALE, CarType.PrivetCar, GearBox.Manual));
                //bl.addTrainee(new Trainee("12345674", "yehonatan", "yosef", new DateTime(1980, 1, 1), "normal_Gaz_School", "eliyahu", 28, "99999999", new Address("shahal", 7, "jerusalem "), Gender.MALE, CarType.PrivetCar, GearBox.Manual));
            }
            FilterPanel1.radioButtonAscending.Checked += RadioButtonAscending_Checked;
            FilterPanel1.radioButtonDescending.Checked += RadioButtonDescending_Checked;
            FilterPanel1.checkBoxFilterMotorcycle.Checked += CheckBoxFilterMotorcycle_Checked;
            FilterPanel1.checkBoxFilterMotorcycle.Unchecked += CheckBoxFilterMotorcycle_Unchecked;
            FilterPanel1.checkBoxFilterprivetCar.Checked += CheckBoxFilterprivetCar_Checked;
            FilterPanel1.checkBoxFilterprivetCar.Unchecked += CheckBoxFilterprivetCar_Unchecked;
            FilterPanel1.checkBoxFilterSmallTruck.Checked += CheckBoxFilterSmallTruck_Checked;
            FilterPanel1.checkBoxFilterSmallTruck.Unchecked += CheckBoxFilterSmallTruck_Unchecked;
            FilterPanel1.checkBoxFilterTruck.Checked += CheckBoxFilterTruck_Checked;
            FilterPanel1.checkBoxFilterTruck.Unchecked += CheckBoxFilterTruck_Unchecked;
        }

        private void CheckBoxFilterTruck_Unchecked(object sender, RoutedEventArgs e)
        {
            truckFilter = (i) => i.CarType != CarType.Truck;
            findAndSort();
        }

        private void CheckBoxFilterTruck_Checked(object sender, RoutedEventArgs e)
        {
            truckFilter = delegate { return true; };
            findAndSort();
        }

        private void CheckBoxFilterSmallTruck_Unchecked(object sender, RoutedEventArgs e)
        {
            smallTruckFilter = (i) => i.CarType != CarType.SmallTruck;
            findAndSort();
        }

        private void CheckBoxFilterSmallTruck_Checked(object sender, RoutedEventArgs e)
        {
            smallTruckFilter = delegate { return true; };
            findAndSort();
        }

        private void CheckBoxFilterprivetCar_Unchecked(object sender, RoutedEventArgs e)
        {
            privetCarFilter = (i) => i.CarType != CarType.PrivetCar;
            findAndSort();
        }

        private void CheckBoxFilterprivetCar_Checked(object sender, RoutedEventArgs e)
        {
            privetCarFilter = delegate { return true; };
            findAndSort();
        }

        private void CheckBoxFilterMotorcycle_Unchecked(object sender, RoutedEventArgs e)
        {
            motorcycleFilter = (i) => i.CarType != CarType.Motorcycle;
            findAndSort();
        }

        private void CheckBoxFilterMotorcycle_Checked(object sender, RoutedEventArgs e)
        {
            motorcycleFilter = delegate { return true; };
            findAndSort();
        }

        private void RadioButtonDescending_Checked(object sender, RoutedEventArgs e)
        {
            findAndSort();
        }

        private void RadioButtonAscending_Checked(object sender, RoutedEventArgs e)
        {
            findAndSort();
        }

        private void TraineeEvent(object sender, EventArgs e)
        {
            allTraineeList = bl.getAllTrainees();
            findAndSort();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {



        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {

            findAndSort();



            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.M
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
        }

        private void PbUpdateTrainee_Click(object sender, RoutedEventArgs e)
        {
            if (traineeDataGrid.SelectedItem == null) return;
            var selectedPerson = (traineeDataGrid.SelectedItem) as Trainee;

            UpdateTraineeWindow updateTraineeWindow = new UpdateTraineeWindow(selectedPerson);

            //  updateTesterWindow.DataContext = selectedPerson;


            updateTraineeWindow.ShowDialog();
        }

        private void PbAddTrainee_Click(object sender, RoutedEventArgs e)
        {
            AddTraineeWindow addTraineeWindow = new AddTraineeWindow();
            addTraineeWindow.ShowDialog();
        }

        private void MenuItem_Click_delete(object sender, RoutedEventArgs e)
        {
            #region message 'are you sure?'
            MessageBoxResult result = MessageBox.Show("are you sure you want to delete the trainee?", "", MessageBoxButton.YesNo, MessageBoxImage.None, MessageBoxResult.Yes);
            switch (result)
            {
                case MessageBoxResult.No:
                    return;
                case MessageBoxResult.Yes:
                    break;
                default:
                    break;

            }
            #endregion

            if (traineeDataGrid.SelectedItem == null) return;
            try
            {
                bl.deleteTrainee((traineeDataGrid.SelectedItem as Trainee));
            }
            catch (Exception msg)
            {

                MessageBox.Show(msg.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void MenuItem_Click_add(object sender, RoutedEventArgs e)
        {
            AddTraineeWindow addTraineeWindow = new AddTraineeWindow();
            addTraineeWindow.ShowDialog();
        }

        private void MenuItem_Click_edit(object sender, RoutedEventArgs e)
        {
            if (traineeDataGrid.SelectedItem == null) return;
            var selectedPerson = (traineeDataGrid.SelectedItem) as Trainee;

            UpdateTraineeWindow updateTraineeWindow = new UpdateTraineeWindow(selectedPerson);

            //  updateTesterWindow.DataContext = selectedPerson;


            updateTraineeWindow.ShowDialog();
        }

        /// <summary>
        /// sort the data grid by the sort mod in the combo box
        /// </summary>
        private void sortContaxDataByComboBox()
        {
            if (ComboBoxSortBy.SelectedItem == null)
            {
                traineeDataGrid.DataContext = currentUseList;
                return;
            }
           
            var allTrainee =currentUseList;
            IOrderedEnumerable<Trainee> sortTrainee = null;
            switch (ComboBoxSortBy.SelectedItem.ToString())
            {
                case "firstName":
                    sortTrainee = from item in allTrainee orderby item.FirstName select item;
                    break;
                case "lastName":
                    sortTrainee = from item in allTrainee orderby item.LastName select item;
                    break;
                case "id":
                    sortTrainee = from item in allTrainee orderby item.Id select item;
                    break;
                case "number of lesson":
                    sortTrainee = from item in allTrainee orderby item.NumberOfLesson select item;
                    break;
                case "gender":
                    sortTrainee = from item in allTrainee orderby item.Gender select item;
                    break;
                case "age":
                    sortTrainee = from item in allTrainee orderby item.Age select item;
                    break;
                case "school name":
                    sortTrainee = from item in allTrainee orderby item.SchoolName select item;
                    break;
                case "car type":
                    sortTrainee = from item in allTrainee orderby item.CarType select item;
                    break;
                case "teacher name":
                    sortTrainee = from item in allTrainee orderby item.TeacherName select item;
                    break;
            }
            if (FilterPanel1.radioButtonDescending.IsChecked == true)
                traineeDataGrid.DataContext = sortTrainee.Reverse();
            else
                traineeDataGrid.DataContext = sortTrainee;
        }

        private void ComboBoxSortBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems == null) return;
            sortContaxDataByComboBox();
        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            findAndSort();
        }

        /// <summary>
        /// search for trainee with id, first name or last name with the string in the sort text box then sort by the sort mod
        /// </summary>
        private void findAndSort()
        {
            carTypeFilter = (i) => truckFilter(i) && smallTruckFilter(i) && privetCarFilter(i) && motorcycleFilter(i);
            if (TextBoxSearch.Text == "")
                currentUseList = (from i in allTraineeList
                                  where carTypeFilter(i)
                                  select i).ToList();
            else
                currentUseList = (from i in allTraineeList
                                  where (i.FirstName.Contains(TextBoxSearch.Text) || i.LastName.Contains(TextBoxSearch.Text) || i.Id.Contains(TextBoxSearch.Text))&&carTypeFilter(i)
                                  select i).ToList();
            sortContaxDataByComboBox();
        }
    }
}
