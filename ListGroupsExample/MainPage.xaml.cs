using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Automation;
using Windows.UI.Xaml.Controls;

namespace ListGroupsExample
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            ContactsListView.ChoosingGroupHeaderContainer += OnChoosingGroupHeaderContainer;
            this.groupedList = GetContactsGrouped();
            ContactsCVS.Source = this.groupedList;
        }

        private ObservableCollection<GroupInfoList> groupedList;

        private void OnChoosingGroupHeaderContainer(ListViewBase sender, ChoosingGroupHeaderContainerEventArgs args)
        {
            var header = new ListViewHeaderItem();
            var group = (GroupInfoList)args.Group;

            AutomationProperties.SetName(header, group.Key);
            AutomationProperties.SetLevel(header, 1);
            AutomationProperties.SetPositionInSet(header, args.GroupIndex + 1);
            AutomationProperties.SetSizeOfSet(header, groupedList.Count);

            args.GroupHeaderContainer = header;
        }

        public static ObservableCollection<GroupInfoList> GetContactsGrouped()
        {
            var list = new ObservableCollection<Contact>();
            list.Add(new Contact("Joe", "Rackham", "Microsoft"));
            list.Add(new Contact("Tiger", "Cross", "Microsoft"));
            list.Add(new Contact("Julie", "Emile", "Google"));
            list.Add(new Contact("Jack", "Morrison", "Google"));

            var query = from item in list

            group item by item.Company into g
            orderby g.Key
            select new GroupInfoList(g) { Key = g.Key };

            return new ObservableCollection<GroupInfoList>(query);
        }

        public class GroupInfoList : List<object>
        {
            public GroupInfoList(IEnumerable<object> items) : base(items)
            {
            }
            public string Key { get; set; }
        }

        public class Contact
        {
            public string FirstName { get; private set; }
            public string LastName { get; private set; }
            public string Company { get; private set; }
            public string Name => FirstName + " " + LastName;

            public Contact(string firstName, string lastName, string company)
            {
                FirstName = firstName;
                LastName = lastName;
                Company = company;
            }
        }
    }
}
