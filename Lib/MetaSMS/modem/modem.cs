

namespace MetaSMS.modem
{
    class modem
    {
        //private SmsSubmitPdu pdu;

        private void sendSMSByModem() 
        {
            //if (ddlModemName.Text == "Select Your Device")
            //{
            //    ScriptMessage("Select Your Phone from Available Devices", MessageType.Warning);
            //    return;
            //}

            //if (txtSMSText.Text == "")
            //{
            //    ScriptMessage("Fill up message box.", MessageType.Warning);
            //    return;
            //}

            //// find port address and baud rate of select device

            //ManagementObjectSearcher moSearcher = null;

            //if (IntPtr.Size == 4)
            //{
            //    moSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_POTSModem WHERE ConfigManagerErrorCode = 0");
            //}

            //else if (IntPtr.Size == 8)
            //{
            //    moSearcher = new ManagementObjectSearcher("SELECT * FROM Win64_POTSModem WHERE ConfigManagerErrorCode = 0");
            //}


            ////foreach (ManagementObject devices in moSearcher.Get())
            ////{
            ////    if (ddlModemName.Text == devices["Caption"].ToString())
            ////    {
            ////        port = devices["AttachedTo"].ToString();
            ////        baudRate = Convert.ToInt32(devices["MaxBaudRateToSerialPort"].ToString());
            ////    }
            ////}


            //LoadModem();
            //testSendingSms();
            //string msgShort = txtSMSText.Text.ToString();

            //if (testConnection == true)
            //{
            //    //txtOutput.Text = "Message Count...";


            //    //load a array
            //    ArrayList arraylist = new ArrayList();
            //    DataTable dt = new DataTable();
            //    dt.Columns.AddRange(new DataColumn[1] { new DataColumn("phone") });
            //    foreach (GridViewRow row in gvContactList.Rows)
            //    {

            //        if (row.RowType == DataControlRowType.DataRow)
            //        {
            //            CheckBox chkRow = (row.Cells[0].FindControl("chkSelect") as CheckBox);
            //            if (chkRow.Checked)
            //            {
            //                string phone = (row.Cells[2].FindControl("lblPhone") as Label).Text;
            //                if (phone != "")
            //                    arraylist.Add(phone);
            //            }


            //        }
            //    }

            //    if (arraylist.Count == 0)
            //    {
            //        ScriptMessage("Please select your contact", MessageType.Warning);
            //        return;
            //    }

            //    // initialize all cell number in array
            //    int successCount = 0;
            //    int failCount = 0;

            //    GsmCommMain comm = new GsmCommMain(port, baudRate, 300);
            //    comm.Open();


            //    foreach (string number in arraylist)
            //    {
            //        try
            //        {
            //            pdu = new SmsSubmitPdu(msgShort, number, "");
            //            comm.SendMessage(pdu);
            //            successCount++;
            //        }
            //        catch
            //        {
            //            failCount++;
            //        }
            //        // lblSendCount.Text = "Message send " + successCount + " times of " + arraylist.Count + " message and message send fail " + failCount;
            //        //ScriptMessage("SMS has Send.", MessageType.Success);
            //    }


            //    comm.Close();
            //    txtSMSText.Text = "";

            //    ScriptMessage("Message successfully send " + successCount + " times and failed " + failCount + " times.", MessageType.Info);

            //    // initialize for loop

            //}
            //testConnection = false;
        }

       
    }
}
