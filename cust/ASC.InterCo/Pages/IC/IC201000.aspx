<%@ Page Language="C#" MasterPageFile="~/MasterPages/ListView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="IC201000.aspx.cs" Inherits="Page_IC201000" Title="InterCo Sites" %>
<%@ MasterType VirtualPath="~/MasterPages/ListView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" TypeName="ASCInterCo.ASCICSiteMaint" PrimaryView="ICSite" SuspendUnloading="False" >
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phL" runat="Server">
	<px:PXGrid ID="grid" runat="server" Height="400px" Width="100%" Style="z-index: 100"
		AllowPaging="True" AllowSearch="True" AdjustPageSize="Auto" DataSourceID="ds" SkinID="Primary" TabIndex="3500" TemporaryFilterCaption="Filter Applied">
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
			<px:PXGridLevel DataKeyNames="ICsiteCD" DataMember="ICSite">
			    <RowTemplate>
                    <px:PXSegmentMask ID="edICsiteCD" runat="server" DataField="ICsiteCD">
                    </px:PXSegmentMask>
                    <px:PXTextEdit ID="edDescription" runat="server" AlreadyLocalized="False" DataField="Description" DefaultLocale="">
                    </px:PXTextEdit>
                    <px:PXCheckBox ID="edIsActive" runat="server" AlreadyLocalized="False" DataField="IsActive" Text="Active">
                    </px:PXCheckBox>
                    <px:PXTextEdit ID="edUrl" runat="server" AlreadyLocalized="False" DataField="Url" DefaultLocale="">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edCompanyKey" runat="server" AlreadyLocalized="False" DataField="CompanyKey" DefaultLocale="">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edBranchCD" runat="server" AlreadyLocalized="False" DataField="BranchCD" DefaultLocale="">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edLogin" runat="server" AlreadyLocalized="False" DataField="Login" DefaultLocale="">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edPassword" runat="server" AlreadyLocalized="False" DataField="Password" DefaultLocale="">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edVersionSegment" runat="server" AlreadyLocalized="False" DataField="VersionSegment" DefaultLocale="">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edMaxAttempts" runat="server" AlreadyLocalized="False" DataField="MaxAttempts" DefaultLocale="">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edTimeOut" runat="server" AlreadyLocalized="False" DataField="TimeOut" DefaultLocale="">
                    </px:PXTextEdit>
                </RowTemplate>
                <Columns>
                    <px:PXGridColumn DataField="ICsiteCD" Width="120px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Description" Width="200px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="IsActive" TextAlign="Center" Type="CheckBox" Width="60px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Url" Width="200px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Login" Width="200px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="CompanyKey" Width="200px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="BranchCD" Width="120px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Password">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="VersionSegment">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="MaxAttempts">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="TimeOut">
                    </px:PXGridColumn>
                </Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="200" />
	</px:PXGrid>
</asp:Content>
