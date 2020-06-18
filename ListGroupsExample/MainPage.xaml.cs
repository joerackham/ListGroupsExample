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
            ContactsCVS.Source = GetContactsGrouped();
        }
        private void OnChoosingGroupHeaderContainer(ListViewBase sender, ChoosingGroupHeaderContainerEventArgs args)
        {
            if (args.GroupHeaderContainer != null)
            {
                AutomationProperties.SetName(args.GroupHeaderContainer, "Foo bar");
            }
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
            public object Key { get; set; }
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
