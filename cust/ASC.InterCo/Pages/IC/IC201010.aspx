<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="IC201010.aspx.cs" Inherits="Page_IC201010" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="ICSiteBAccount" TypeName="ASCInterCo.ASCICSiteBAccountMaint">
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" 
		Width="100%" DataMember="ICSiteBAccount" TabIndex="10600">
		<Template>
			<px:PXLayoutRule runat="server" StartColumn="True"/>
		    <px:PXSegmentMask ID="edBAccountID" runat="server" DataField="BAccountID">
            </px:PXSegmentMask>
            <px:PXSegmentMask ID="edICsiteID" runat="server" DataField="ICsiteID">
            </px:PXSegmentMask>
            <px:PXLayoutRule runat="server" StartColumn="True">
            </px:PXLayoutRule>
            <px:PXTextEdit ID="edICRemoteAcctCD" runat="server" AlreadyLocalized="False" DataField="RemoteAcctCD">
            </px:PXTextEdit>
            <px:PXCheckBox ID="edIsActive" runat="server" AlreadyLocalized="False" DataField="IsActive" Text="Is Active">
            </px:PXCheckBox>
		</Template>
	    <CallbackCommands>
            <Save PostData="Self" />
        </CallbackCommands>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" Runat="Server">
    <px:PXGrid ID="grid" runat="server" DataSourceID="ds" Style="z-index: 100" 
		Width="100%" Height="150px" SkinID="PrimaryInquire" TabIndex="7700" TemporaryFilterCaption="Filter Applied">
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
			<px:PXGridLevel DataKeyNames="BAccountID" DataMember="Settings">
			    <RowTemplate>
                    <px:PXSegmentMask ID="edBAccountID" runat="server" DataField="BAccountID">
                    </px:PXSegmentMask>
                    <px:PXNumberEdit ID="edICSortOrder" runat="server" AlreadyLocalized="False" DataField="ICSortOrder" DefaultLocale="">
                    </px:PXNumberEdit>
                    <px:PXCheckBox ID="edIsActive" runat="server" AlreadyLocalized="False" DataField="IsActive" Text="Active">
                    </px:PXCheckBox>
                    <px:PXSegmentMask ID="edSourceSiteID" runat="server" DataField="SourceSiteID">
                    </px:PXSegmentMask>
                    <px:PXTextEdit ID="edSourceObject" runat="server" AlreadyLocalized="False" DataField="SourceObject" DefaultLocale="">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edSourceType" runat="server" AlreadyLocalized="False" DataField="SourceType" DefaultLocale="">
                    </px:PXTextEdit>
                    <px:PXSegmentMask ID="edDestinationSiteID" runat="server" DataField="DestinationSiteID">
                    </px:PXSegmentMask>
                    <px:PXTextEdit ID="edDestinationObject" runat="server" AlreadyLocalized="False" DataField="DestinationObject" DefaultLocale="">
                    </px:PXTextEdit>
                    <px:PXTextEdit ID="edDestinationType" runat="server" AlreadyLocalized="False" DataField="DestinationType" DefaultLocale="">
                    </px:PXTextEdit>
                </RowTemplate>
                <Columns>
                    <px:PXGridColumn DataField="BAccountID" Width="120px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="ICSortOrder" TextAlign="Right">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="IsActive" TextAlign="Center" Type="CheckBox" Width="60px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="SourceSiteID" TextAlign="Right">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="SourceObject" Width="120px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="SourceType" Width="120px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="DestinationSiteID" TextAlign="Right">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="DestinationObject" Width="120px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="DestinationType" Width="120px">
                    </px:PXGridColumn>
                </Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="150" />
	</px:PXGrid>
</asp:Content>
