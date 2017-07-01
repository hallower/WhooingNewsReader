namespace WhooingNewsReader.Models
{
    public class Item : BaseDataObject
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
    }
}
