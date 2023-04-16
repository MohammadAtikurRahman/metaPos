using MetaPOS.Admin.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetaPOS.Account.Helper
{
    public class Version
    {
        public decimal UpgradeVersion(decimal version)
        {
            try
            {
                var sqlOperation = new SqlOperation();

                if (version > 5.0M || version <= 6.1M)
                {
                    version = 1.0M;
                }

                if (version <= 1.0M)
                {
                    string query = "BEGIN TRANSACTION "

                        + "IF COL_LENGTH('CashReportInfo','payType') IS NULL "
                        + "BEGIN ALTER TABLE CashReportInfo ADD payType NVARCHAR(255) NULL END "

                        + "IF COL_LENGTH('CashReportInfo','payDescr') IS NULL "
                        + "BEGIN ALTER TABLE CashReportInfo ADD payDescr NVARCHAR(255) NULL END "

                        + "IF COL_LENGTH('StockStatusInfo','payType') IS NULL "
                        + "BEGIN ALTER TABLE StockStatusInfo ADD payType NVARCHAR(255) NULL END "

                        + "IF COL_LENGTH('StockStatusInfo','payDescr') IS NULL "
                        + "BEGIN ALTER TABLE StockStatusInfo ADD payDescr NVARCHAR(255) NULL END "

                        + "IF COL_LENGTH('StockStatusInfo','serialNo') IS NULL "
                        + "BEGIN ALTER TABLE StockStatusInfo ADD serialNo NVARCHAR(250) NULL END "

                        + "IF COL_LENGTH('SaleInfo','serialNo') IS NULL "
                        + "BEGIN ALTER TABLE SaleInfo ADD serialNo NVARCHAR(250) NULL END "

                        + "IF COL_LENGTH('SlipInfo','serialNo') IS NULL "
                        + "BEGIN ALTER TABLE SlipInfo ADD serialNo NVARCHAR(250) NULL END "

                        + "IF COL_LENGTH('SettingInfo','isDisplaySerialNo') IS NULL "
                        + "BEGIN ALTER TABLE SettingInfo ADD isDisplaySerialNo CHAR(1) NULL END "

                        + "IF COL_LENGTH('SaleInfo','invoiceType') IS NULL "
                        + "BEGIN ALTER TABLE SaleInfo ADD invoiceType NVARCHAR(5) NOT NULL DEFAULT(('0')) END "

                        + "IF COL_LENGTH('StockStatusInfo','invoiceType') IS NULL "
                        + "BEGIN ALTER TABLE StockStatusInfo ADD invoiceType NVARCHAR(5) NOT NULL DEFAULT(('0')) END "

                        + " IF OBJECT_ID('dbo.QtyManagement', 'U') IS NULL "
                        + " BEGIN CREATE TABLE QtyManagement(Id INT IDENTITY (1, 1) NOT NULL, "
                        + " productId NVARCHAR (30) NOT NULL, "
                        + " stockQty NVARCHAR (30) NOT NULL, "
                        + " storeId INT DEFAULT ((0)) NOT NULL, "
                        + " roleId INT DEFAULT((0)) NOT NULL, "
                        + " branchId INT DEFAULT((30)) NOT NULL, "
                        + " groupId INT DEFAULT((0)) NOT NULL, "
                        + " entryDate DATETIME NOT NULL DEFAULT(getdate()), "
                        + " updateDate DATETIME NOT NULL DEFAULT(getdate()), "
                        + " PRIMARY KEY CLUSTERED (Id ASC)) END "

                        + "IF COL_LENGTH('SettingInfo','monthWaseSalesReport') IS NULL "
                        + "BEGIN ALTER TABLE SettingInfo ADD monthWaseSalesReport numeric(1) NULL END "

                        + "IF COL_LENGTH('SettingInfo','challanPaperSize') IS NULL "
                        + "BEGIN ALTER TABLE SettingInfo ADD challanPaperSize char(1) NOT NULL DEFAULT(('0')) END "

                        + "IF COL_LENGTH('SettingInfo','displayChallanAddress') IS NULL "
                        + "BEGIN ALTER TABLE SettingInfo ADD displayChallanAddress char(1) NOT NULL DEFAULT(('0')) END "

                        + "IF COL_LENGTH('SettingInfo','displayChallanLocation') IS NULL "
                        + "BEGIN ALTER TABLE SettingInfo ADD displayChallanLocation char(1) NOT NULL DEFAULT(('0')) END "

                        + "IF COL_LENGTH('SubscriptionInfo','active') IS NULL "
                        + "BEGIN ALTER TABLE SubscriptionInfo ADD active char(1) NOT NULL DEFAULT(('1')) END "

                        + "IF COL_LENGTH('StockInfo','pCode') IS NULL "
                        + "BEGIN ALTER TABLE StockInfo ADD pCode NVARCHAR(50) NULL END "

                        + "IF COL_LENGTH('SettingInfo','tokenRcpt') IS NULL "
                        + "BEGIN ALTER TABLE SettingInfo ADD tokenRcpt numeric(1) NULL END "

                        //table userlogsInfo
                        + " IF OBJECT_ID('dbo.UserLogsInfo', 'U') IS NULL "
                        + " BEGIN CREATE TABLE UserLogsInfo(Id INT IDENTITY (1, 1) NOT NULL, "
                        + " roleId INT DEFAULT((0)) NOT NULL, "
                        + " branchId INT DEFAULT((30)) NOT NULL, "
                        + " userRight NVARCHAR(10) DEFAULT(('Branch')) NOT NULL, "
                        + " email NVARCHAR(50) DEFAULT(('test.com')) NOT NULL, "
                        + " ipAddress NVARCHAR(50) DEFAULT((0)) NOT NULL, "
                        + " loginDate DATETIME NOT NULL DEFAULT(getdate()), "
                        + " PRIMARY KEY CLUSTERED (Id ASC)) END "

                        

                        + "COMMIT";

                    sqlOperation.executeQuery(query);
                    version = 1.0M;
                }

                if (version <= 1.1M)
                {
                    string query = "BEGIN TRANSACTION "

                        + "IF COL_LENGTH('RoleInfo','isSecureAccount') IS NULL "
                        + "BEGIN ALTER TABLE RoleInfo ADD isSecureAccount BIT NOT NULL DEFAULT(('0')) END "

                        + "IF COL_LENGTH('RoleInfo','paymentMode') IS NULL "
                        + "BEGIN ALTER TABLE RoleInfo ADD paymentMode char(1) NOT NULL DEFAULT(('1')) END "

                        + "IF COL_LENGTH('SettingInfo','paymentMode') IS NULL "
                        + "BEGIN ALTER TABLE SettingInfo ADD paymentMode char(1) NOT NULL DEFAULT(('1')) END "

                        + "IF COL_LENGTH('SubscriptionInfo','storeId') IS NULL "
                        + "BEGIN ALTER TABLE SubscriptionInfo ADD storeId INT NOT NULL DEFAULT(('0')) END "

                        + "IF COL_LENGTH('SubscriptionInfo','name') IS NULL "
                        + "BEGIN ALTER TABLE SubscriptionInfo ADD name NVARCHAR(255) END "

                        + "IF COL_LENGTH('SubscriptionInfo','description') IS NULL "
                        + "BEGIN ALTER TABLE SubscriptionInfo ADD description NVARCHAR(255) END "

                        + "IF COL_LENGTH('SubscriptionInfo','paymentMode') IS NULL "
                        + "BEGIN ALTER TABLE SubscriptionInfo ADD paymentMode CHAR(1) NOT NULL DEFAULT(('1')) END "

                        + "IF COL_LENGTH('SubscriptionInfo','cashout') IS NULL "
                        + "BEGIN ALTER TABLE SubscriptionInfo ADD cashout decimal(12,2) NOT NULL DEFAULT((0)) END "

                        + "IF COL_LENGTH('SubscriptionInfo','cashin') IS NULL "
                        + "BEGIN ALTER TABLE SubscriptionInfo ADD cashin decimal(12,2) NOT NULL DEFAULT((0)) END "


                        + "COMMIT";

                    sqlOperation.executeQuery(query);
                    version = 1.1M;

                }

                return version;

            }
            catch (Exception)
            {
                return version;
            }
        }
    }
}