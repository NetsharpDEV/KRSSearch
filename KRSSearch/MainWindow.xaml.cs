
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
            var associationData = _associationRepo.GetData();
            dataGrid.DataContext = associationData;
            _headquarterList = associationData.Select(x => x.HeadQuarter).Distinct().ToList();
            _representativesList = associationData.Select(x => x.RepresentationName).Distinct().ToList();
            _legalFormList = associationData.Select(x => x.LegalForm).Distinct().ToList();
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
            var emailComboValue = emailCombo.SelectedValue;
            var wwwComboValue = webSiteCombo.SelectedValue;
            var headquarterValue = comboHeadQuarter.SelectedValue;
            var legalFormVal = legalFormCombo.SelectedValue;
            var representativeVal = representationCombo.SelectedValue;
            var searchName = textBoxName.Text;
            var searchRegon = textBoxRegon;
        }
        private List<KRSItemModel> FilterData(string email, string www, string headquarter, string legalForm, string representative, string name, string regon, DateTime date)
        {            
            return _associationData.Where(x => x.Email == email && x.Name == name && x.HeadQuarter == headquarter && x.LegalForm == legalForm && x.RepresentationName == representative && x.Regon == regon).ToList();
        }
    }
}
