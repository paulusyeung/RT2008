#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using RT2008.DAL;
using System.Data.SqlClient;
using System.Configuration;

#endregion

namespace RT2008.Product
{
    /// <summary>
    /// Part of Product Wizard: Order
    /// </summary>
    public partial class ProductWizard_Order : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductWizard_Order"/> class.
        /// </summary>
        /// <param name="productId">The product id.</param>
        public ProductWizard_Order(Guid productId)
        {
            InitializeComponent();
            this.ProductId = productId;

            BindPOHistory();
        }

        #region Properties
        /// <summary>
        /// the productId
        /// </summary>
        private Guid productId = System.Guid.Empty;

        /// <summary>
        /// Gets or sets the product id.
        /// </summary>
        /// <value>The product id.</value>
        public Guid ProductId
        {
            get
            {
                return productId;
            }
            set
            {
                productId = value;
            }
        }
        #endregion

        /// <summary>
        /// Binds the PO history.
        /// </summary>
        private void BindPOHistory()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = BuildQueryString();
            cmd.CommandTimeout = Common.Config.CommandTimeout;
            cmd.CommandType = System.Data.CommandType.Text;

            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ListViewItem objItem = this.lvPurchaseHistory.Items.Add(reader.GetDateTime(0).ToString(RT2008.SystemInfo.Settings.GetDateFormat())); // TxDate
                    objItem.SubItems.Add(reader.GetString(1)); // Supplier Code
                    objItem.SubItems.Add(reader.GetString(2)); // Supplier Name
                    objItem.SubItems.Add(reader.GetString(3)); // TxNumber
                    objItem.SubItems.Add(reader.GetString(4)); // OrderNumber
                    objItem.SubItems.Add(reader.GetDecimal(5).ToString("n2")); // UnitCost
                    objItem.SubItems.Add(reader.GetDecimal(6).ToString("n2")); // NetUnitCost 
                }
            }
        }

        /// <summary>
        /// Builds the query string.
        /// </summary>
        /// <returns></returns>
        private string BuildQueryString()
        {
            string query = @"SELECT [TxDate],[SupplierCode],[SupplierName],[TxNumber],[OrderNumber],[UnitCost],[NetUnitCost]
                            FROM [vwProductPOHistory]
                            WHERE ProductId = '" + this.ProductId.ToString() + @"'
                            ORDER BY [TxDate]";

            return query;
        }
    }
}