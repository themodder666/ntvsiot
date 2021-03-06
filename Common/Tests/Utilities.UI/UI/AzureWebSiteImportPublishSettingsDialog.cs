﻿//*********************************************************//
//    Copyright (c) Microsoft. All rights reserved.
//    
//    Apache 2.0 License
//    
//    You may obtain a copy of the License at
//    http://www.apache.org/licenses/LICENSE-2.0
//    
//    Unless required by applicable law or agreed to in writing, software 
//    distributed under the License is distributed on an "AS IS" BASIS, 
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or 
//    implied. See the License for the specific language governing 
//    permissions and limitations under the License.
//
//*********************************************************//

using System;
using System.Windows.Automation;

namespace TestUtilities.UI {
    public class AzureWebSiteImportPublishSettingsDialog : AutomationDialog {
        public AzureWebSiteImportPublishSettingsDialog(VisualStudioApp app, AutomationElement element)
            : base(app, element) {
        }

        public void ClickImportFromWindowsAzureWebSite() {
            WaitForInputIdle();
            ImportFromWindowsAzureWebSiteRadioButton().Select();
        }

        public void ClickSignOut() {
            WaitForInputIdle();
            var sign = new AutomationWrapper(FindByAutomationId("AzureSigninControl"));
            sign.ClickButtonByName("Sign Out");
        }

        public AzureManageSubscriptionsDialog ClickImportOrManageSubscriptions() {
            WaitForInputIdle();
            var importElement = ImportSubscriptionsHyperlink();
            if (importElement == null) {
                importElement = ManageSubscriptionsHyperlink();
            }
            importElement.GetInvokePattern().Invoke();
            return new AzureManageSubscriptionsDialog(App, AutomationElement.FromHandle(App.WaitForDialogToReplace(Element)));
        }

        public AzureWebSiteCreateDialog ClickNew() {
            WaitForInputIdle();
            ClickButtonByAutomationId("NewButton");
            return new AzureWebSiteCreateDialog(App, AutomationElement.FromHandle(App.WaitForDialogToReplace(Element)));
        }

        public void ClickOK() {
            WaitForInputIdle();
            WaitForClosed(TimeSpan.FromSeconds(10.0), () => ClickButtonByAutomationId("OKButton"));
        }

        private AutomationElement ImportFromWindowsAzureWebSiteRadioButton() {
            return Element.FindFirst(TreeScope.Descendants,
                new AndCondition(
                    new PropertyCondition(AutomationElement.AutomationIdProperty, "ImportLabel"),
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.RadioButton)
                )
            );
        }

        private AutomationElement ImportSubscriptionsHyperlink() {
            return Element.FindFirst(TreeScope.Descendants,
                new AndCondition(
                    new PropertyCondition(AutomationElement.NameProperty, "Import subscriptions"),
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Hyperlink)
                )
            );
        }

        private AutomationElement ManageSubscriptionsHyperlink() {
            return Element.FindFirst(TreeScope.Descendants,
                new AndCondition(
                    new PropertyCondition(AutomationElement.NameProperty, "Manage subscriptions"),
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Hyperlink)
                )
            );
        }
    }
}
