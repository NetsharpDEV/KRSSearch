
using KRSSearch.Logic;
using KRSSearch.Logic.Models;
using KRSSearch.Logic.Utilities;
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

namespace KRSSearch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IAssociationRepository _associationRepo;
        private List<string> _headquarterList;
        private List<string> _legalFormList;
        private List<string> _representativesList;
        private List<KRSItemModel> _associationData;

        public MainWindow(IAssociationRepository repo)
        {
            _associationRepo = repo;
            _headquarterList = new List<string>();
            _representativesList = new List<string>();
            _legalFormList = new List<string>();
            _associationData = new List<KRSItemModel>();
            InitializeComponent();
          
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            _associationData = _associationRepo.GetData();
            dataGrid.DataContext = _associationData;
            _headquarterList = _associationData.Select(x => x.HeadQuarter).Distinct().ToList();
            _representativesList = _associationData.Select(x => x.RepresentationName).Distinct().ToList();
            _legalFormList = _associationData.Select(x => x.LegalForm).Distinct().ToList();
            initComboboxValues();
        }
        private void initComboboxValues()
        {
            #region Email ComboBox
            emailCombo.SelectedValue = "Key";
            emailCombo.DisplayMemberPath = "Value";
            emailCombo.Items.Add(new KeyValuePair<EmailStatus, string>(EmailStatus.All, EmailStatus.All.GetDescription()));
            emailCombo.Items.Add(new KeyValuePair<EmailStatus, string>(EmailStatus.WithEmail, EmailStatus.WithEmail.GetDescription()));
            emailCombo.Items.Add(new KeyValuePair<EmailStatus, string>(EmailStatus.WithoutEmail, EmailStatus.WithoutEmail.GetDescription()));
            emailCombo.SelectedIndex = 0;
            #endregion
            #region WWW ComboBox
            webSiteCombo.SelectedValue = "Key";
            webSiteCombo.DisplayMemberPath = "Value";
            webSiteCombo.Items.Add(new KeyValuePair<WebSiteStatus, string>(WebSiteStatus.All, WebSiteStatus.All.GetDescription()));
            webSiteCombo.Items.Add(new KeyValuePair<WebSiteStatus, string>(WebSiteStatus.WithWWW, WebSiteStatus.WithWWW.GetDescription()));
            webSiteCombo.Items.Add(new KeyValuePair<WebSiteStatus, string>(WebSiteStatus.WithoutWWW, WebSiteStatus.WithoutWWW.GetDescription()));
            webSiteCombo.SelectedIndex = 0;
            #endregion
            #region HeadQuarter ComboBox
            comboHeadQuarter.Items.Add("Wszystkie");
            _headquarterList.ForEach(item =>
            {
                comboHeadQuarter.Items.Add(item);
            });           
            comboHeadQuarter.SelectedIndex = 0;
            #endregion
            #region LegalForm ComboBox
            legalFormCombo.Items.Add("Wszystkie");
            _legalFormList.ForEach(item =>
            {
                legalFormCombo.Items.Add(item);
            });
            legalFormCombo.SelectedIndex = 0;
            #endregion
            #region Representations ComboBox
            representationCombo.Items.Add("Wszystkie");
            _representativesList.ForEach(item =>
            {
                representationCombo.Items.Add(item);
            });
            representationCombo.SelectedIndex = 0;
            #endregion
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            var emailComboValue = (KeyValuePair<EmailStatus, string>)emailCombo.SelectedValue;
            var wwwComboValue = (KeyValuePair<WebSiteStatus, string>)webSiteCombo.SelectedValue;
            var headquarterValue = comboHeadQuarter.SelectedValue.ToString();
            var legalFormVal = legalFormCombo.SelectedValue.ToString();
            var representativeVal = representationCombo.SelectedValue.ToString();
            var searchName = textBoxName.Text;
            var searchRegon = textBoxRegon.Text;
            var datePickerVal = DateRegistration.SelectedDate;

            dataGrid.DataContext = FilterData(emailComboValue.Key, wwwComboValue.Key, headquarterValue, legalFormVal, representativeVal, searchName, searchRegon, datePickerVal);
        }
        private List<KRSItemModel> FilterData(EmailStatus email, WebSiteStatus www, string headquarter, string legalForm, string representative, string name, string regon, DateTime? date)
        {
            List<KRSItemModel> list = _associationData;
            if (email == EmailStatus.WithEmail)
                list = list.Where(x => x.Email != string.Empty && x.Email != null).ToList();
            if (email == EmailStatus.WithoutEmail)
                list = list.Where(x => x.Email == string.Empty || x.Email == null).ToList();

            if (www == WebSiteStatus.WithWWW)
                list = list.Where(x => x.WebSite != string.Empty && x.WebSite != null).ToList();
            if (www == WebSiteStatus.WithoutWWW)
                list = list.Where(x => x.WebSite == string.Empty || x.WebSite == null).ToList();

            if (!string.IsNullOrEmpty(headquarter) && headquarter != "Wszystkie")
                list = list.Where(x => x.HeadQuarter == headquarter).ToList();

            if (!string.IsNullOrEmpty(legalForm) && legalForm != "Wszystkie")
                list = list.Where(x => x.LegalForm == legalForm).ToList();

            if (!string.IsNullOrEmpty(representative) && representative != "Wszystkie")
                list = list.Where(x => x.RepresentationName == representative).ToList();

            if (!string.IsNullOrEmpty(name))
                list = list.Where(x => x.Name.ToUpper() == name.ToUpper()).ToList();

            if (!string.IsNullOrEmpty(regon))
                list = list.Where(x => x.Regon.ToUpper() == regon.ToUpper()).ToList();


            return list;



        }
    }
}
