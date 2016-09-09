<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AjaxAccordion.aspx.vb" Inherits="AjaxAccordion" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ajax Control Demo</title>
    <link rel="Stylesheet" href="SS_ILIFE.css" type="text/css" />
    <script language="javascript" type="text/javascript" src="Script/ScriptJS.js">
    </script>
<script language="javascript" type='text/javascript'>
    function cancelClick() {
        //var label = $get('ctl00_SampleContent_Label1');
        //label.innerHTML = 'You hit cancel in the Confirm dialog on ' + (new Date()).localeFormat("T") + '.';
    }
</script>
</head>
<body>
    <form id="form1" runat="server">
    
    <div>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        
    </div>
    
    <div align="center" class="tbl_cont">
        <div class="demoheading">ConfirmButton Demonstration</div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                
                    <asp:LinkButton ID="LinkButton" runat="server" OnClick="Button_Click">Click Me</asp:LinkButton>
                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" 
                        TargetControlID="LinkButton"
                        ConfirmText="Are you sure you want to click the LinkButton?" 
                        OnClientCancel="cancelClick" />
                    <br />
                    <br />
                    <asp:Button ID="Button" runat="server" Text="Click Me" OnClick="Button_Click" /><br />
                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" 
                        TargetControlID="Button"
                        OnClientCancel="cancelClick"
                        DisplayModalPopupID="ModalPopupExtender1" />
                    <br />
                    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button" PopupControlID="PNL" OkControlID="ButtonOk" CancelControlID="ButtonCancel" BackgroundCssClass="modalBackground" />
                    <asp:Panel ID="PNL" runat="server" style="display:none; width:200px; background-color:White; border-width:2px; border-color:Black; border-style:solid; padding:20px;">
                        Are you sure you want to click the Button?
                        <br /><br />
                        <div style="text-align:right;">
                            <asp:Button ID="ButtonOk" runat="server" Text="OK" />
                            <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" />
                        </div>
                    </asp:Panel>
               
                <asp:Label ID="Label4" Text="Status:" ForeColor="Red" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    

    <!-- start header -->
    <div id="div_header" align="right">
        &nbsp;<a class="HREF_MENU2" href="#" onclick="javascript:JSDO_RETURN('MENU_GL.aspx')">Returns to Previous Page</a>&nbsp;
    </div>
    
        <div align="center" class="demoarea" style="margin: 0px; padding: 0px; border: 1px; width: 1000px;">
        <div align="center" class="demoheading">Ajax Accordion Demonstration</div>
        <asp:Accordion ID="MyAccordion" runat="server" SelectedIndex="-1"
            HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
            ContentCssClass="accordionContent" FadeTransitions="false" FramesPerSecond="40" 
            TransitionDuration="250" AutoSize="None" RequireOpenedPane="false" 
            SuppressHeaderPostbacks="true" Width="100%" Height="100%">
           <Panes>
            <asp:AccordionPane ID="AccordionPane0" runat="server">
                <Header><a href="" class="accordionLink">Endorsement</a></Header>
                <Content>
                    <table class="tbl_cont" align="center">
                        <caption>Group Life Policy Details</caption>
                            <tr>
                                <td align="left" valign="top" class="td_menu_x">
                                    <table align="center" border="0" cellspacing="0" class="tbl_menu_new">
                                            <tr>
                                                <td align="center" colspan="4" valign="top">
                                                    <asp:button id="cmdNew_ASP" Font-Bold="true" Font-Size="Large" runat="server" text="New Data" OnClientClick="JSNew_ASP();"></asp:button>
                                                    &nbsp;&nbsp;<asp:button id="cmdSave_ASP" Font-Bold="true" Font-Size="Large" runat="server" text="Save Data" OnClientClick="JSSave_ASP();"></asp:button>
                                                    &nbsp;&nbsp;<asp:button id="cmdDelete_ASP" Font-Bold="true" Font-Size="Large" runat="server" text="Delete Data" OnClientClick="JSDelete_ASP();"></asp:button>
                                                    &nbsp;&nbsp;<asp:button id="cmdPrint_ASP" Enabled="false" Font-Bold="true" Font-Size="Large" runat="server" text="Print"></asp:button>
                                                    &nbsp;&nbsp;Status:&nbsp;<asp:textbox id="txtAction" Visible="true" ForeColor="Gray" runat="server" EnableViewState="False" Width="50px"></asp:textbox>
                                                    <!-- &nbsp;&nbsp;<a href="javascript:window.close();">Close...</a> -->
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" colspan="4" valign="top"><hr /></td>
                                            </tr>

                                            <tr>
                                                <td align="left" valign="top"><asp:Label ID="lblQuote_Num" Text="Policy No:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top"><asp:TextBox ID="txtQuote_Num" runat="server"></asp:TextBox></td>
                                                <td align="left" valign="top"><asp:Label ID="Label1" Text="Transaction Date:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top"><asp:TextBox ID="txtTrans_Date" runat="server"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="top"><asp:Label ID="lblStart_Date" Text="Start Date:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top"><asp:TextBox ID="txtStart_Date" runat="server"></asp:TextBox></td>
                                                <td align="left" valign="top"><asp:Label ID="lblEnd_Date" Text="Expiry Date:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top"><asp:TextBox ID="txtEnd_Date" runat="server"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="top"><asp:Label ID="Label2" Text="Scheme Type:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">
                                                    <select id="selScheme_Type" class="selScheme" runat="server" onchange="selScheme_Type_Change(this)">
                                                        <option value="*">select option</option>
                                                        <option value="001">Scheme A</option>
                                                        <option value="002">Scheme B</option>
                                                    </select>
                                                    &nbsp;<asp:TextBox ID="txtScheme_Type" runat="server" Width="40px"></asp:TextBox>&nbsp;
                                                </td>
                                                <td align="left" valign="top"><asp:Label ID="Label3" Text="Scheme Type:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">
                                                    <select id="selScheme_Server" class="selScheme" runat="server" onchange="jsProc(this,this.form.TabContainer1$TabPanel1$selScheme_Server)">
                                                        <option value="*">select option</option>
                                                        <option value="001">Scheme A</option>
                                                        <option value="002">Scheme B</option>
                                                    </select>
                                                    &nbsp;<asp:TextBox ID="txtScheme_Server" runat="server" Width="40px"></asp:TextBox>&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" colspan="4" valign="top">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td align="left" colspan="4" valign="top">
                                                    <asp:Label ID="lblMsg" ForeColor="Red" Font-Size="Small" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td align="left" colspan="4" valign="top">&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>                
                </Content>
            </asp:AccordionPane>    

            <asp:AccordionPane ID="AccordionPane1" runat="server">
                <Header><a href="" class="accordionLink">1. Accordion</a></Header>
                <Content>
                    <p>
                    The Accordion is a web control that allows you to provide multiple panes and display them one at a time.
                    It is like having several <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/CollapsiblePanel/CollapsiblePanel.aspx" Text="CollapsiblePanels" />
                    where only one can be expanded at a time.  The Accordion is implemented as a web control that contains
                    AccordionPane web controls. Each AccordionPane control has a template for its Header and its Content.
                    We keep track of the selected pane so it stays visible across postbacks.
                    </p>
                </Content>
            </asp:AccordionPane>    
            <asp:AccordionPane ID="AccordionPane2" runat="server">
                <Header><a href="" class="accordionLink">2. AutoSize</a></Header>
                <Content>
                    <p>It also supports three AutoSize modes so it can fit in a variety of layouts.</p>
                    <ul>
                        <li><b>None</b> - The Accordion grows/shrinks without restriction.  This can cause other elements
                            on your page to move up and down with it.</li>
                        <li><b>Limit</b> - The Accordion never grows larger than the value specified by its Height
                            property.  This will cause the content to scroll if it is too large to be displayed.</li>
                        <li><b>Fill</b> - The Accordion always stays the exact same size as its Height property.  This
                            will cause the content to be expanded or shrunk if it isn't the right size.</li>
                    </ul>
                    
                </Content>
            </asp:AccordionPane>
            <asp:AccordionPane ID="AccordionPane3" runat="server">
                <Header><a href="" class="accordionLink">3. Control or Extender</a></Header>
                <Content>
                    The Accordion is written using an extender like most of the other extenders in the AJAX Control Toolkit.
                    The extender expects its input in a very specific hierarchy of container elements (like divs), so
                    the Accordion and AccordionPane web controls are used to generate the expected input for the extender.
                    The extender can also be used on its own if you provide it appropriate input.
                </Content>
            </asp:AccordionPane>
            <asp:AccordionPane ID="AccordionPane4" runat="server">
                <Header><a href="" class="accordionLink">4. What is ASP.NET AJAX?</a></Header>
                <Content>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/AJAX.gif" AlternateText="ASP.NET AJAX" ImageAlign="right" />
                    <%= GetContentFillerText()%>
                </Content>
            </asp:AccordionPane>
            </Panes>
        </asp:Accordion>
        
        </div>
        
        Fade Transitions: <input id="fade" type="checkbox" onclick="toggleFade();" value="false" /><br />
        AutoSize: <select id="autosize" onchange="changeAutoSize();">
            <option selected="selected">None</option>
            <option>Limit</option>
            <option>Fill</option>
        </select>
        
        <script language="javascript" type="text/javascript">
            function toggleFade() {
                var behavior = $find('ctl00_SampleContent_MyAccordion_AccordionExtender');
                if (behavior) {
                    behavior.set_FadeTransitions(!behavior.get_FadeTransitions());
                }
            }
            function changeAutoSize() {
                var behavior = $find('ctl00_SampleContent_MyAccordion_AccordionExtender');
                var ctrl = $get('autosize');
                if (behavior) {
                    var size = 'None';
                    switch (ctrl.selectedIndex) {
                        case 0 :
                            behavior.get_element().style.height = 'auto';
                            size = AjaxControlToolkit.AutoSize.None;
                            break;
                        case 1 :
                            behavior.get_element().style.height = '400px';
                            size = AjaxControlToolkit.AutoSize.Limit;
                            break;
                        case 2 :
                            behavior.get_element().style.height = '400px';
                            size = AjaxControlToolkit.AutoSize.Fill;
                            break;
                    }
                    behavior.set_AutoSize(size);
                }
                if (document.focus) {
                    document.focus();
                }
            }
        </script>

        <hr />    
        <div align="left" style="margin: 0px; padding: 0px; border: 1px; width: 1000px;">
        <asp:Panel ID="Description_HeaderPanel" runat="server" Style="cursor: pointer;">
        <div class="heading">
            <asp:ImageButton ID="Description_ToggleImage" runat="server" ImageUrl="~/images/collapse.jpg" AlternateText="collapse" />
            Tabs Description
        </div>
    </asp:Panel>
    <asp:Panel ID="Description_ContentPanel" runat="server" Style="overflow: hidden;">
        <p>
            TabContainer is an ASP.NET AJAX Control which creates a set of Tabs that can be
            used to organize page content. A TabContainer is a host for a number of TabPanel
            controls.
            <br /><br />
            Each TabPanel defines its HeaderText or HeaderTemplate as well as a ContentTemplate
            that defines its content. The most recent tab should remain selected after a postback,
            and the Enabled state of tabs should remain after a postback as well.
        </p>
    </asp:Panel>
    
    <asp:Panel ID="Properties_HeaderPanel" runat="server" Style="cursor: pointer;">
        <div class="heading">
            <asp:ImageButton ID="Properties_ToggleImage" runat="server" ImageUrl="~/images/expand.jpg" AlternateText="expand" />
            Tabs Properties
        </div>
    </asp:Panel>

    <asp:Panel ID="Properties_ContentPanel" runat="server" Style="overflow: hidden;" Height="0px">
        <p>The control above is initialized with this code.  The <em>italic</em> properties are optional:</p>
        <pre>&lt;ajaxToolkit:TabContainer runat="server" 
        <em>OnClientActiveTabChanged</em>="ClientFunction" 
        <em>Height</em>="150px"&gt;
        <strong>&lt;ajaxToolkit:TabPanel</strong> runat="server" 
        <em>HeaderText</em>="Signature and Bio"
        &lt;ContentTemplate&gt;
            ...
        &lt;/ContentTemplate&gt;
        <strong>/&gt;</strong>
        &lt;/ajaxToolkit:TabContainer&gt;</pre>
        <b>TabContainer Properties</b>
        <ul>
            <li><strong>ActiveTabChanged (Event)</strong> - Fired on the server side when a tab
                is changed after a postback</li>
            <li><strong>OnClientActiveTabChanged</strong> - The name of a javascript function to
                attach to the client-side tabChanged event</li>
            <li><strong>CssClass</strong> - A css class override used to define a custom look and
                feel for the tabs. See the Tabs Theming section for more details.</li>
            <li><strong>ActiveTabIndex</strong> - The first tab to show</li>
            <li><strong>Height</strong> - sets the height of the body of the tabs (does not include
                the TabPanel headers)</li>
            <li><strong>Width</strong> - sets the width of the body of the tabs</li>
            <li><strong>ScrollBars</strong> - Whether to display scrollbars (None, Horizontal,
                Vertical, Both, Auto) in the body of the TabContainer</li>
            <li><strong>TabStripPlacement</strong> - Whether to render the tabs on top of the container or below 
                (Top, Bottom) </li>
        </ul>
        <b>TabPanel Properties</b>
        <ul>
            <li><strong>Enabled</strong> - Whether to display the Tab for the TabPanel by default.
                This can be changed on the client.</li>
            <li><strong>OnClientClick</strong> - The name of a javascript function to attach to
                the client-side click event of the tab.</li>
            <li><strong>HeaderText</strong> - The text to display in the Tab</li>
            <li><strong>HeaderTemplate</strong> - A TemplateInstance.Single ITemplate to use to
                render the header</li>
            <li><strong>ContentTemplate</strong> - A TemplateInstance.Single ITemplate to use to
                render the body</li>
        </ul>
    </asp:Panel>

    <asp:Panel runat="server" ID="TabCSS_HeaderPanel" Style="cursor: pointer;">
        <div class="heading">
            <asp:ImageButton ID="TabCSS_ToggleImage" runat="server" ImageUrl="~/images/collapse.jpg"
                AlternateText="collapse" />
            Tabs Theming
        </div>
    </asp:Panel>
    <asp:Panel runat="server" ID="TabCSS_ContentPanel" Style="overflow: hidden;" Height="0px">
        You can change the look and feel of Tabs using the Tabs CssClass property. Tabs
        has a predefined set of CSS classes that can be overridden. It has a default style
        which is embedded as a WebResource and is a part of the Toolkit assembly that has
        styles set for all the sub-classes. You can find the default styles in the Toolkit
        solution in the <strong>"AjaxControlToolkit\Tabs\Tabs.css"</strong> file. If your
        CssClass does not provide values for any of those then it falls back to the default
        value. In the example above the default style is used. To customize the same the
        user would have to set the CssClass property to the name of the CSS style and define
        the styles for the individual classes so that the various elements in a Tab control
        can be styled accordingly. For example if the CssClass property was set to "CustomTabStyle"
        this is how the css to style the tab header would look:
        <pre>
        .CustomTabStyle .ajax__tab_header {
        font-family:verdana,tahoma,helvetica;
        font-size:11px;
        background:url(images/tab-line.gif) repeat-x bottom;
        }</pre>
        <strong>Tabs Css classes</strong>
        <br />
        <ul>
            <li><strong>.ajax__tab_header:</strong>
                A container element that wraps all of the tabs at the top of the TabContainer.
                Child CSS classes:.ajax__tab_outer. </li>
            <li><strong>.ajax__tab_outer:</strong> An outer element of a tab, often used to set
                the left-side background image of the tab.Child CSS classes: .ajax__tab_inner.
            </li>
            <li><strong>.ajax__tab_inner:</strong> An inner element of a tab, often used to set
                the right-side image of the tab. Child CSS classes:.ajax__tab_tab. </li>
            <li><strong>.ajax__tab_tab:</strong> An element of the tab that
                contains the text content. Child CSS classes:none.</li>
            <li><strong>.ajax__tab_body</strong>: A container element that wraps the area where
                a TabPanel is displayed. Child CSS classes: none.</li>
            <li><strong>.ajax__tab_hover</strong> . This is applied to a tab when the mouse is hovering
                over. Child CSS classes:.ajax__tab_outer. </li>
            <li><strong>.ajax__tab_active</strong>: This is applied to a tab when it is the currently
                selected tab. Child CSS classes:.ajax__tab_outer. </li>
        </ul>
    </asp:Panel>

    <asp:CollapsiblePanelExtender ID="cpeDescription" runat="Server" TargetControlID="Description_ContentPanel"
        ExpandControlID="Description_HeaderPanel" CollapseControlID="Description_HeaderPanel"
        Collapsed="False" ImageControlID="Description_ToggleImage" />
    <asp:CollapsiblePanelExtender ID="cpeProperties" runat="Server" TargetControlID="Properties_ContentPanel"
        ExpandControlID="Properties_HeaderPanel" CollapseControlID="Properties_HeaderPanel"
        Collapsed="True" ImageControlID="Properties_ToggleImage" />
    <asp:CollapsiblePanelExtender ID="cpeTabsCSS" runat="Server" TargetControlID="TabCSS_ContentPanel"
        ExpandControlID="TabCSS_HeaderPanel" CollapseControlID="TabCSS_HeaderPanel" Collapsed="True"
        ImageControlID="TabCSS_ToggleImage" />

    </div>

    </form>
</body>
</html>
