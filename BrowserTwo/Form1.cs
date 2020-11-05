using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrowserTwo
{
    public partial class BrowserBody : Form
    {    

        public BrowserBody()
        {
            InitializeComponent();
        }

        private void BrowserBody_Load(object sender, EventArgs e)
        {
            CefSettings settings = new CefSettings();
            // initialize
            Cef.Initialize(settings);
            this.textUrl.Text = "www.google.com";
            ChromiumWebBrowser chrome = new ChromiumWebBrowser(this.textUrl.Text);
            chrome.Parent = this.tabControl.SelectedTab;
            
            chrome.Dock = DockStyle.Fill;
            chrome.AddressChanged += Chrome_AddressChanged;
            chrome.TitleChanged += Chrome_TitleChanged;
        }

        private void Chrome_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            this.Invoke(new MethodInvoker(()=>
            {
                this.textUrl.Text = e.Address;
            }));
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            ChromiumWebBrowser chrome = this.tabControl.SelectedTab.Controls[0] as ChromiumWebBrowser; 
            if (!string.IsNullOrEmpty(this.textUrl.Text) && chrome != null) 
                chrome.Load(this.textUrl.Text);
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            ChromiumWebBrowser chrome = this.tabControl.SelectedTab.Controls[0] as ChromiumWebBrowser;
            if(chrome != null) chrome.Refresh();
        }

        private void forwardButton_Click(object sender, EventArgs e)
        {
            ChromiumWebBrowser chrome = this.tabControl.SelectedTab.Controls[0] as ChromiumWebBrowser;
            if (chrome != null)
            {
                if (chrome.CanGoForward)
                    chrome.Forward();
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            ChromiumWebBrowser chrome = this.tabControl.SelectedTab.Controls[0] as ChromiumWebBrowser;
            if (chrome != null)
            {
                if (chrome.CanGoBack)
                    chrome.Back();
            }
        }

        private void BrowserBody_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }

        private void newTabButton_Click(object sender, EventArgs e)
        {
            TabPage tab = new TabPage();
            tab.Text = "New tab";
            tabControl.Controls.Add(tab);
            tabControl.SelectTab(tabControl.TabCount - 1);
            ChromiumWebBrowser chrome = new ChromiumWebBrowser("https://www.google.com");
            chrome.Parent = tab;
            chrome.Dock = DockStyle.Fill;
            this.textUrl.Text = "https://www.google.com";
            chrome.AddressChanged += Chrome_AddressChanged;
            chrome.TitleChanged += Chrome_TitleChanged;
        }

        private void Chrome_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                this.tabControl.SelectedTab.Text = e.Title;
            }));
        }
    }
}
