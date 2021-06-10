﻿#region < HEADER AREA >
// *---------------------------------------------------------------------------------------------*
//   Form ID      : PP_ActureOutPut
//   Form Name    : 자재 재고관리 
//   Name Space   : KFQS_Form
//   Created Date : 2020/08
//   Made By      : DSH
//   Description  : 
// *---------------------------------------------------------------------------------------------*
#endregion

#region < USING AREA >
using System;
using System.Data;
using DC_POPUP;

using DC00_assm;
using DC00_WinForm;

using Infragistics.Win.UltraWinGrid;
#endregion

namespace KFQS_Form
{
    public partial class PP_ActureOutPut : DC00_WinForm.BaseMDIChildForm
    {

        #region < MEMBER AREA >
        DataTable dtTemp        = new DataTable(); // 
        UltraGridUtil _GridUtil    = new UltraGridUtil();  //그리드 객체 생성
        Common _Common             = new Common();
        string plantCode           = LoginInfo.PlantCode;

        #endregion


        #region < CONSTRUCTOR >
        public PP_ActureOutPut()
        {
            InitializeComponent();
        }
        #endregion


        #region < FORM EVENTS >
        private void PP_ActureOutPut_Load(object sender, EventArgs e)
        {
            #region ▶ GRID ◀
            _GridUtil.InitializeGrid(this.txtWokerName, true, true, false, "", false);
            _GridUtil.InitColumnUltraGrid(txtWokerName, "PLANTCODE",      "공장",     true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left,    true, false);
            _GridUtil.InitColumnUltraGrid(txtWokerName, "ITEMCODE",       "품목",     true, GridColDataType_emu.VarChar,    140, 120, Infragistics.Win.HAlign.Left,    true, false);
            _GridUtil.InitColumnUltraGrid(txtWokerName, "ITEMNAME",       "품목명",   true, GridColDataType_emu.VarChar,    140, 120, Infragistics.Win.HAlign.Left,    true, false);
            _GridUtil.InitColumnUltraGrid(txtWokerName, "MATLOTNO",       "LOTNO",     true, GridColDataType_emu.VarChar,   120, 120, Infragistics.Win.HAlign.Left,    true, false);
            _GridUtil.InitColumnUltraGrid(txtWokerName, "WHCODE",         "입고창고", true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left,    true, false);
            _GridUtil.InitColumnUltraGrid(txtWokerName, "STOCKQTY",       "재고수량", true, GridColDataType_emu.Double,     100, 120, Infragistics.Win.HAlign.Right,   true, false);
            _GridUtil.InitColumnUltraGrid(txtWokerName, "UNITCODE",       "단위",     true, GridColDataType_emu.VarChar,    100, 120, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(txtWokerName, "CUSTCODE",       "거래처",   true, GridColDataType_emu.VarChar,    100, 120, Infragistics.Win.HAlign.Left,    true, false);
            _GridUtil.InitColumnUltraGrid(txtWokerName, "CUSTNAME",       "거래처명", true, GridColDataType_emu.VarChar,    100, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.SetInitUltraGridBind(txtWokerName);
            #endregion

            #region ▶ COMBOBOX ◀
            dtTemp = _Common.Standard_CODE("PLANTCODE");  // 사업장
            //Common.FillComboboxMaster(this.cboPlantCode, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
            UltraGridUtil.SetComboUltraGrid(this.txtWokerName, "PLANTCODE", dtTemp, "CODE_ID", "CODE_NAME");

            dtTemp = _Common.Standard_CODE("UNITCODE");     //단위
            UltraGridUtil.SetComboUltraGrid(this.txtWokerName, "UNITCODE", dtTemp, "CODE_ID", "CODE_NAME");

            dtTemp = _Common.Standard_CODE("WHCODE");     //입고 창고
            UltraGridUtil.SetComboUltraGrid(this.txtWokerName, "WHCODE", dtTemp, "CODE_ID", "CODE_NAME");

            // 품목코드 
            //FP  : 완제품
            //OM  : 외주가공품
            //R/M : 원자재
            //S/M : 부자재(H / W)
            //SFP : 반제품
            dtTemp = _Common.GET_ItemCodeFERT_Code("R/M");
            //Common.FillComboboxMaster(this.cboItemCode, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");

            #endregion

            #region ▶ POP-UP ◀
            #endregion

            #region ▶ ENTER-MOVE ◀
           // cboPlantCode.Value = plantCode;
            #endregion
        }
        #endregion


        #region < TOOL BAR AREA >
        public override void DoInquire()
        {
            DoFind();
        }
        private void DoFind()
        {
            DBHelper helper = new DBHelper(false);
            try
            {
                string sPlantCode = Convert.ToString(cboPlantCode_H.Value);
                string sWorkcentercode = Convert.ToString(cboWorkcenterCode.Value);
                string sStartDate = string.Format("{0.yyyy-MM-dd}", dtpStart.Value);
                string sEndDate = string.Format("{0.yyyy-MM-dd}", dtpEnd.Value);
                string sOrderNo = Convert.ToString(txtOderNo.Text);

               
                base.DoInquire();
                _GridUtil.Grid_Clear(txtWokerName);

              
                dtTemp = helper.FillTable("07PP_ActureOutput_S1", CommandType.StoredProcedure
                                    , helper.CreateParameter("PLANTCODE",   sPlantCode,  DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("WORKERCENTCODE", sWorkcentercode,   DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("STARTDATE", sStartDate,   DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("ENDDATE", sEndDate,   DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("ODERNO", sOrderNo,   DbType.String, ParameterDirection.Input)                                    
                                    );

               this.ClosePrgForm();
                this.txtWokerName.DataSource = dtTemp;
            }
            catch (Exception ex)
            {
                ShowDialog(ex.ToString(),DialogForm.DialogType.OK);    
            }
            finally
            {
                helper.Close();
            }
        }
        /// <summary>
        /// ToolBar의 신규 버튼 클릭
        /// </summary>
        public override void DoNew()
        {
            
        }
        /// <summary>
        /// ToolBar의 삭제 버튼 Click
        /// </summary>
        public override void DoDelete()
        {   
           
        }
        /// <summary>
        /// ToolBar의 저장 버튼 Click
        /// </summary>
        public override void DoSave()
        {
        }
        #endregion

        private void ultraButton1_Click(object sender, EventArgs e)
        {
            // 바코드 발행
            if (txtWokerName.ActiveRow == null) return; // 선택된 행이 없을 경우 종료
            DataRow drRow = ((DataTable)this.txtWokerName.DataSource).NewRow();
            drRow["ITEMCODE"] = Convert.ToString(this.txtWokerName.ActiveRow.Cells["ITEMCODE"].Value);
            drRow["ITEMNAME"] = Convert.ToString(this.txtWokerName.ActiveRow.Cells["ITEMNAME"].Value);
            drRow["CUSTNAME"] = Convert.ToString(this.txtWokerName.ActiveRow.Cells["CUSTNAME"].Value);
            drRow["STOCKQTY"] = Convert.ToString(this.txtWokerName.ActiveRow.Cells["STOCKQTY"].Value);
            drRow["MATLOTNO"] = Convert.ToString(this.txtWokerName.ActiveRow.Cells["MATLOTNO"].Value);
            drRow["UNITCODE"] = Convert.ToString(this.txtWokerName.ActiveRow.Cells["UNITCODE"].Value);

            // 바코드 디자인 선언
            Report_LotBacode repBarCode             = new Report_LotBacode();
            // 레포트 북 선언
            Telerik.Reporting.ReportBook reportBook = new Telerik.Reporting.ReportBook();
            // 바코드 디자이너에 디자인 등록
            repBarCode.DataSource = drRow;
            // 레포트 북에 디자이너 등록
            reportBook.Reports.Add(repBarCode);

            // 미리보기 창 활성화
            ReportViewer reportViewer = new ReportViewer(reportBook, 1);
            reportViewer.ShowDialog();
        }

        private void ultraButton2_Click(object sender, EventArgs e)
        {

        }

        private void ultraLabel9_Click(object sender, EventArgs e)
        {

        }

        private void ultraTextEditor3_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}




