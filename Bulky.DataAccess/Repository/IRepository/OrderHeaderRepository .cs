using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAcess.Data;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private ApplicationDbContext _db;
        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
       

        public void Update(OrderHeader obj)
        {
            _db.OrderHeaders.Update(obj);
        }

        public void UpdateStatus(int ID, string orderStatus, string? paymentStatus = null)
        {
            var orderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.ID == ID);
            if (orderFromDb != null)
            {
                orderFromDb.OrderStatus = orderStatus;
                if (!string.IsNullOrEmpty(paymentStatus))
                {
                    orderFromDb.PaymentStatus = paymentStatus;
                }
            }
        }

        public void UpdateStripePaymentID(int ID, string sessionID, string paymentIntentID)
        {
            var orderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.ID == ID);
            if(!string.IsNullOrEmpty(sessionID)) {
                orderFromDb.SessionID= sessionID;
            }
            if(!string.IsNullOrEmpty(paymentIntentID))
            {
                orderFromDb.PaymentIntentID= paymentIntentID;
                orderFromDb.PaymentDate = DateTime.Now;
            }
        }
    }
}
    