using System.ComponentModel;

namespace CSharpCourseWork.Models
{
    //Class User is class that admins work with
    internal class User : INotifyPropertyChanged
    {
        public static int _id = 0;
        private int _thisId;
        private string _firstName;
        private string _secondName;
        private string _thirdName;
        private string _liveAddress;
        private string _phoneNumber;
        private string _infoAboutUser;
        public int ThisId
        {
            get { return _thisId; }
            set {
                if (_thisId == value)
                {
                    return;
                }
                _thisId = value;
                OnPropertyChanged("ThisId");
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName == value)
                {
                    return;
                }
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        public string SecondName
        {
            get { return _secondName; }
            set
            {
                if (_secondName == value)
                {
                    return;
                }
                _secondName = value;
                OnPropertyChanged("SecondName");
            }
        }
        public string ThirdName
        {
            get { return _thirdName; }
            set
            {
                if (_thirdName == value)
                {
                    return;
                }
                _thirdName = value;
                OnPropertyChanged("ThirdName");
            }
        }
        public string LiveAddress
        {
            get { return _liveAddress; }
            set
            {
                if (_liveAddress == value)
                {
                    return;
                }
                _liveAddress = value;
                OnPropertyChanged("LiveAddress");
            }
        }
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (_phoneNumber == value)
                {
                    return;
                }
                _phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }
        public string InfoAboutUser
        {
            get { return _infoAboutUser; }
            set
            {
                if (_infoAboutUser == value)
                {
                    return;
                }
                _infoAboutUser = value;
                OnPropertyChanged("InfoAboutUser");
            }
        }

        //Standart constructor
        public User()
        {
            _id += 1;
            ThisId = _id;
        }

        //Parametric constructor
        public User(int thisId, string firstName, string secondName, string thirdName, string liveAddress, string phoneNumber, string infoAboutUser)
        {
            ThisId = thisId;
            FirstName = firstName;
            SecondName = secondName;
            ThirdName = thirdName;
            LiveAddress = liveAddress;
            PhoneNumber = phoneNumber;
            InfoAboutUser = infoAboutUser;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
