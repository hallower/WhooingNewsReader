using System;
using System.Collections;

namespace NewsReader.Models
{
    public class Item : BaseDataObject, IComparable
    {
        string bbs_id = string.Empty;
        public string Bbs_id
        {
            get { return bbs_id; }
            set { SetProperty(ref bbs_id, value); }
        }

        string subject = string.Empty;
        public string Subject
        {
            get { return subject; }
            set { SetProperty(ref subject, value); }
        }

        string description = string.Empty;
        public string Latest
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        string icon = string.Empty;
        public string Icon
        {
            get { return icon; }
            set { SetProperty(ref icon, value); }
        }

        public int CompareTo(object obj)
        {
            if (obj is Item item)
            {
                if (int.TryParse(bbs_id, out int xvalue) &&
                    int.TryParse(item.bbs_id, out int yvalue))
                {
                    return yvalue - xvalue;
                }
            }
            return this.GetHashCode().CompareTo(obj.GetHashCode());
        }
    }
}
