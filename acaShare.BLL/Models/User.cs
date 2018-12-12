using acaShare.BLL.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace acaShare.BLL.Models
{
    public partial class User
    {
        public User(string identityUserId, string username, DateTime registerDate) : this()
        {
            IdentityUserId = identityUserId;
            Username = username;
            RegisterDate = registerDate;
        }

        protected User()
        {
            Comments = new HashSet<Comment>();
            DeleteRequests = new HashSet<DeleteRequest>();
            EditRequests = new HashSet<EditRequest>();
            Favorites = new HashSet<Favorites>();
            ApprovedMaterials = new HashSet<Material>();
            CreatedMaterials = new HashSet<Material>();
            UpdatedMaterials = new HashSet<Material>();
            UsersInUniversity = new HashSet<UserInUniversity>();
            Notifications = new HashSet<Notification>();
            HandledDeleteRequests = new HashSet<DeleteRequest>();
        }
        
        public int UserId { get; private set; }
        public DateTime RegisterDate { get; private set; }
        public string IdentityUserId { get; private set; }
        public string Username { get; private set; }

        public virtual ICollection<Comment> Comments { get; private set; }
        public virtual ICollection<DeleteRequest> DeleteRequests { get; private set; }
        public virtual ICollection<EditRequest> EditRequests { get; private set; }
        public virtual ICollection<Favorites> Favorites { get; private set; }
        public virtual ICollection<Material> ApprovedMaterials { get; private set; }
        public virtual ICollection<Material> CreatedMaterials { get; private set; }
        public virtual ICollection<Material> UpdatedMaterials { get; private set; }
        public virtual ICollection<UserInUniversity> UsersInUniversity { get; private set; }
        public virtual ICollection<Notification> Notifications { get; private set; }
        public virtual ICollection<DeleteRequest> HandledDeleteRequests { get; private set; }

        public void Notify(NotificationType notificationType, IDictionary<string, string> data)
        {
            var notifier = new Notifier();
            Notification newNotification = notifier.CreateNotificationForUser(notificationType, data, this);

            if (data["MaterialId"] != null)
            {
                newNotification.MaterialId = int.Parse(data["MaterialId"]);
            }
            
            Notifications.Add(newNotification);
        }
        
        public void MarkNotificationAsRead(Notification notification)
        {
            Notifications.FirstOrDefault(n => n == notification)?.MarkAsRead();
        }

        public void RemoveNotification(Notification notification)
        {
            Notifications.Remove(notification);
        }

        public bool IsMaterialFavorite(Material material)
        {
            return Favorites.Any(f => f.Material == material);
        }

        public void ToggleFavorite(Material material)
        {
            Favorites favorite = new Favorites
            {
                Material = material,
                User = this
            };

            if (IsMaterialFavorite(material))
            {
                var favoriteToRemove = Favorites.First(f => f.Material == material);
                Favorites.Remove(favoriteToRemove);
            }
            else
            {
                Favorites.Add(favorite);
            }
        }

        public ICollection<Material> GetFavoriteMaterials()
        {
            return Favorites.Select(f => f.Material).ToList();
        }
    }
}
