using System.Collections.Generic;
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

            ContactsCVS.Source = GetContactsGrouped();
        }

        private void OnChoosingGroupHeaderContainer(ListViewBase sender, ChoosingGroupHeaderContainerEventArgs args)
        {
            var company = ((IGrouping<string, Contact>)args.Group).Key;

            if (args.GroupHeaderContainer is null)
            {
                args.GroupHeaderContainer = new ListViewHeaderItem();
            }

            AutomationProperties.SetName(args.GroupHeaderContainer, company);
        }

        public static IReadOnlyCollection<IGrouping<string, Contact>> GetContactsGrouped()
        {
            var contacts = new[]
            {
                new Contact("Joe", "Rackham", "Microsoft"),
                new Contact("John", "Smith", "Amazon"),
                new Contact("Tiger", "Cross", "Microsoft"),
                new Contact("Julie", "Emile", "Google"),
                new Contact("Jack", "Morrison", "Google")
            };

            return contacts
                .GroupBy(c => c.Company)
                .OrderBy(g => g.Key)
                .ToList().AsReadOnly();
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
