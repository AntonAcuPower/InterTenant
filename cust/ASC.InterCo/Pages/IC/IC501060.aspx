<%@ Page Language="C#" MasterPageFile="~/MasterPages/ListView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="IC501060.aspx.cs" Inherits="Page_IC501060" Title="Intercompany Transactions" %>
<%@ MasterType VirtualPath="~/MasterPages/ListView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">

   <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" TypeName="ASCInterCo.ASCICTranProcess" PrimaryView="PagePrimaryView">

       <CallbackCommands/>

   </px:PXDataSource>

</asp:Content>

<asp:Content ID="cont2" ContentPlaceHolderID="phL" runat="Server">

<px:PXGrid ID="grid" runat="server" Height="400px" Width="100%" Style="z-index: 100" ActionsPosition="Top" FilesIndicator="False" NoteIndicator="False" AllowSearch="True" DataSourceID="ds" SkinID="PrimaryInquire" TabIndex="700" TemporaryFilterCaption="Filter Applied">

<EmptyMsg ComboAddMessage="No records found.
Try to change filter or modify parameters above to see records here." NamedComboMessage="No records found as &#39;{0}&#39;.
Try to change filter or modify parameters above to see records here." NamedComboAddMessage="No records found as &#39;{0}&#39;.
Try to change filter or modify parameters above to see records here." FilteredMessage="No records found.
Try to change filter to see records here." FilteredAddMessage="No records found.
Try to change filter to see records here." NamedFilteredMessage="No records found as &#39;{0}&#39;.
Try to change filter to see records here." NamedFilteredAddMessage="No records found as &#39;{0}&#39;.
Try to change filter to see records here." AnonFilteredMessage="No records found.
Try to change filter to see records here." AnonFilteredAddMessage="No records found.
Try to change filter to see records here."></EmptyMsg>
<Levels>
<px:PXGridLevel DataMember="PagePrimaryView">
    <RowTemplate>
        <px:PXCheckBox ID="edSelected" runat="server" AlreadyLocalized="False" DataField="Selected" Text="Selected">
        </px:PXCheckBox>
        <px:PXNumberEdit ID="edICtranid" runat="server" AlreadyLocalized="False" DataField="ICtranid" DefaultLocale="">
        </px:PXNumberEdit>
        <px:PXNumberEdit ID="edSourceSiteID" runat="server" AlreadyLocalized="False" DataField="SourceSiteID">
        </px:PXNumberEdit>
        <px:PXTextEdit ID="edSourceObject" runat="server" AlreadyLocalized="False" DataField="SourceObject">
        </px:PXTextEdit>
        <px:PXTextEdit ID="edSourceType" runat="server" AlreadyLocalized="False" DataField="SourceType">
        </px:PXTextEdit>
        <px:PXTextEdit ID="edSourceRef" runat="server" AlreadyLocalized="False" DataField="SourceRef">
        </px:PXTextEdit>
        <px:PXNumberEdit ID="edDestinationSiteID" runat="server" AlreadyLocalized="False" DataField="DestinationSiteID">
        </px:PXNumberEdit>
        <px:PXTextEdit ID="edDestinationObject" runat="server" AlreadyLocalized="False" DataField="DestinationObject">
        </px:PXTextEdit>
        <px:PXTextEdit ID="edDestinationType" runat="server" AlreadyLocalized="False" DataField="DestinationType">
        </px:PXTextEdit>
        <px:PXTextEdit ID="edDestinationRef" runat="server" AlreadyLocalized="False" DataField="DestinationRef">
        </px:PXTextEdit>
        <px:PXNumberEdit ID="edFailedAttempts" runat="server" AlreadyLocalized="False" DataField="FailedAttempts">
        </px:PXNumberEdit>
    </RowTemplate>
   <Columns>
       <px:PXGridColumn DataField="Selected" TextAlign="Center" Type="CheckBox" Width="30" AllowCheckAll="True" AllowSort="False" AllowMove="False"/>
       <px:PXGridColumn DataField="ICtranid" TextAlign="Right">
       </px:PXGridColumn>
       <px:PXGridColumn DataField="SourceSiteID" TextAlign="Right" Width="200px">
       </px:PXGridColumn>
       <px:PXGridColumn DataField="SourceObject" Width="120px">
       </px:PXGridColumn>
       <px:PXGridColumn DataField="SourceType" Width="120px">
       </px:PXGridColumn>
       <px:PXGridColumn DataField="SourceRef" Width="120px">
       </px:PXGridColumn>
       <px:PXGridColumn DataField="DestinationSiteID" TextAlign="Right" Width="200px">
       </px:PXGridColumn>
       <px:PXGridColumn DataField="DestinationObject" Width="120px">
       </px:PXGridColumn>
       <px:PXGridColumn DataField="DestinationType" Width="120px">
       </px:PXGridColumn>
       <px:PXGridColumn DataField="DestinationRef" Width="120px">
       </px:PXGridColumn>
       <px:PXGridColumn DataField="FailedAttempts" TextAlign="Right">
       </px:PXGridColumn>
   </Columns>
</px:PXGridLevel>
</Levels>
   <ActionBar PagerVisible="Bottom">
       <PagerSettings Mode="NextPrevFirstLast"/>
   </ActionBar>
<AutoSize Container="Window" Enabled="True" MinHeight="200" />
</px:PXGrid>
</asp:Content>