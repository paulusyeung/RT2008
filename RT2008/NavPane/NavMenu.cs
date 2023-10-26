using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Xml;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Common.Resources;
using Gizmox.WebGUI.Forms;
using System.IO;
using System.Collections.Generic;

namespace RT2008.NavPane
{
    public class NavMenu
    {
        private static XmlDocument NavDocument
        {
            get
            {
                XmlDocument oXmlDoc = new XmlDocument();

                string xmlFile = string.Format("~/Resources/Menu/NavMenu_{0}.xml", RT2008.Controls.UserUtility.PermissionLevel());

                string _Source = System.Web.HttpContext.Current.Server.MapPath(xmlFile);
                if (File.Exists(_Source))
                {
                    oXmlDoc.Load(_Source);

                    return oXmlDoc;
                }

                return null;
            }
        }

        public static void FillNavPane(ref NavigationTabs navTabs)
        {
            XmlDocument oXmlDoc = NavDocument;

            if (oXmlDoc != null)
            {
                navTabs.TabPages.Clear();

                XmlNodeList oNodeList = oXmlDoc.DocumentElement.ChildNodes;
                foreach (XmlNode oNode in oNodeList)
                {
                    XmlNode oCurNode = oNode;

                    if (oCurNode.HasChildNodes)
                    {
                        NavigationTab tabPage = new NavigationTab();

                        tabPage.Image = new IconResourceHandle(oCurNode.Attributes["ImageUrl"].Value);
                        tabPage.Location = new System.Drawing.Point(4, 22);
                        tabPage.Name = "tab" + oCurNode.Attributes["Caption"].Value;
                        tabPage.Size = new System.Drawing.Size(142, 316);
                        tabPage.TabIndex = 0;
                        tabPage.Text = RT2008.Controls.Utility.Dictionary.GetWord(oCurNode.Attributes["Caption"].Value);
                        tabPage.Tag = oCurNode.Attributes["Caption"].Value;

                        navTabs.Controls.Add(tabPage);

                        switch (oCurNode.Name.ToLower())
                        {
                            case "inventory":
                                RT2008.NavPane.InventoryNav navInvt = new RT2008.NavPane.InventoryNav();
                                navInvt.Dock = DockStyle.Fill;
                                tabPage.Controls.Add(navInvt);
                                break;
                            case "purchasing":
                                RT2008.NavPane.PurchasingNav navPurchasing = new RT2008.NavPane.PurchasingNav();
                                navPurchasing.Dock = DockStyle.Fill;
                                tabPage.Controls.Add(navPurchasing);
                                break;
                            case "membermgmt":
                                RT2008.NavPane.MemberMgmtNav navMemberMgmt = new RT2008.NavPane.MemberMgmtNav();
                                navMemberMgmt.Dock = DockStyle.Fill;
                                tabPage.Controls.Add(navMemberMgmt);
                                break;
                            case "settings":
                                RT2008.NavPane.SettingsNav navSettings = new RT2008.NavPane.SettingsNav();
                                navSettings.Dock = DockStyle.Fill;
                                tabPage.Controls.Add(navSettings);
                                break;
                            case "product":
                                RT2008.NavPane.ProductNav navProduct = new RT2008.NavPane.ProductNav();
                                navProduct.Dock = DockStyle.Fill;
                                tabPage.Controls.Add(navProduct);
                                break;
                        }
                    }
                }
            }
        }

        public static void FillNavTree(string filter, TreeNodeCollection oNav)
        {
            XmlDocument oXmlDoc = NavDocument;

            if (oXmlDoc != null)
            {
                XmlNodeList oNodeList = oXmlDoc.DocumentElement.ChildNodes;
                foreach (XmlNode oNode in oNodeList)
                {
                    XmlNode oCurNode = oNode;

                    if (oCurNode.HasChildNodes && oCurNode.Name.ToLower() == filter)
                    {
                        FillTreeMenu(oNode, oNav);
                    }
                }
            }
        }

        // 2007.10.23 paulus: I use FillTree_ to skips the first node
        private static void FillTreeMenu(XmlNode node, TreeNodeCollection parentnode)
        {
            // Add all the children of the current node to the treeview
            foreach (XmlNode tmpchildnode in node.ChildNodes)
            {
                FillTree(tmpchildnode, parentnode);
            }
        }

        private static void FillTree(XmlNode node, TreeNodeCollection parentnode)
        {
            TreeNodeCollection tmpNodes = AddNodeToTree(node, parentnode);

            // Add all the children of the current node to the treeview
            foreach (XmlNode tmpchildnode in node.ChildNodes)
            {
                FillTree(tmpchildnode, tmpNodes);
            }
        }

        private static TreeNodeCollection AddNodeToTree(XmlNode node, TreeNodeCollection parentnode)
        {
            TreeNode newchildnode = CreateTreeNodeFromXmlNode(node);
            // if nothing to add, return the parent item
            if (newchildnode == null) return parentnode;
            // add the newly created tree node to its parent
            if (parentnode != null) parentnode.Add(newchildnode);
            return newchildnode.Nodes;
        }

        private static Gizmox.WebGUI.Forms.TreeNode CreateTreeNodeFromXmlNode(XmlNode node)
        {
            TreeNode tmptreenode = new TreeNode();
            if ((node.HasChildNodes) && (node.FirstChild.Value != null))
            {
                tmptreenode = new TreeNode(node.Name);
                TreeNode tmptreenode2 = new TreeNode(node.FirstChild.Value);
                tmptreenode.Nodes.Add(tmptreenode2);
            }
            else if (node.NodeType != XmlNodeType.CDATA)
            {
                List<string> keyWords = new List<string>();
                keyWords.Add("STKCODE");
                keyWords.Add("APPENDIX1");
                keyWords.Add("APPENDIX2");
                keyWords.Add("APPENDIX3");
                keyWords.Add("CLASS1");
                keyWords.Add("CLASS2");
                keyWords.Add("CLASS3");
                keyWords.Add("CLASS4");
                keyWords.Add("CLASS5");
                keyWords.Add("CLASS6");

                string label = string.Empty;
                if (keyWords.Exists(key => key == node.Attributes["Caption"].Value))
                {
                    label = System.Web.HttpUtility.UrlDecode(RT2008.SystemInfo.Settings.GetSystemLabelByKey(node.Attributes["Caption"].Value));
                }
                else
                {
                    label = System.Web.HttpUtility.UrlDecode(RT2008.Controls.Utility.Dictionary.GetWord(node.Attributes["Caption"].Value));
                }

                if (node.HasChildNodes)
                {
                    Font font = new Font("Tahoma", 11, FontStyle.Bold, GraphicsUnit.Pixel);

                    TreeNode oNode = new TreeNode();

                    oNode.Label = label;
                    oNode.NodeFont = font;
                    oNode.IsExpanded = false;

                    tmptreenode = oNode;
                }
                else
                {
                    Font font = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);

                    TreeNode oNode = new TreeNode();

                    oNode.Label = label;
                    oNode.Tag = node.Attributes["Tag"].Value;
                    oNode.Image = new IconResourceHandle(node.Attributes["ImageUrl"].Value);
                    oNode.NodeFont = font;

                    tmptreenode = oNode;
                }
            }
            return tmptreenode;
        }
    }
}
