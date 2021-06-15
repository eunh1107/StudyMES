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
using System.Data.OleDb;
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
        DataTable dtTemp = new DataTable(); // 
        UltraGridUtil _GridUtil = new UltraGridUtil();  //그리드 객체 생성
        Common _Common = new Common();
        string plantCode = LoginInfo.PlantCode;

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
            _GridUtil.InitializeGrid(this.grid1, true, true, false, "", false);
            _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE", "공장", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ORDERNO", "작업지시 번호", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE", "품목 코드", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "PLANQTY", "계획수량", true, GridColDataType_emu.Double, 130, 130, Infragistics.Win.HAlign.Right, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "PRODQTY", "양품수량", true, GridColDataType_emu.Double, 130, 130, Infragistics.Win.HAlign.Right, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "BADQTY", "불량수량", true, GridColDataType_emu.Double, 130, 130, Infragistics.Win.HAlign.Right, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "UNITCODE", "단위", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "MATLOTNO", "투입LOT", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "COMPONENT", "투입품목", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "COMPONENTQTBY", "투입 수량", true, GridColDataType_emu.Double, 130, 130, Infragistics.Win.HAlign.Right, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "CUNITCODE", "투입단위", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERCODE", "작업장", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKSTATUSCODE", "상태코드", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKSTATUS", "상태", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKER", "작업자", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKERNAME", "작업자명", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "STARTDATE", "최초 가동 시작 시간", true, GridColDataType_emu.DateTime24, 160, 130, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ENDDATE", "작업지시  종료 시간", true, GridColDataType_emu.DateTime24, 160, 130, Infragistics.Win.HAlign.Left, true, false);


            #endregion

            #region ▶ COMBOBOX ◀
            dtTemp = _Common.Standard_CODE("PLANTCODE");  // 사업장
            Common.FillComboboxMaster(this.cboPlantCode_H, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
            UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", dtTemp, "CODE_ID", "CODE_NAME");

            // 작업장 마스터 데이터 가져와서 임시 테이블에 등록
            dtTemp = _Common.GET_Workcenter_Code(); // 작업장

            //콤보박스 컨트롤에 가져온 데이터 등록
            Common.FillComboboxMaster(this.cboWorkcenterCode, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
            // 그리드 해당 컬럼에 콤보박스 유형으로 셋팅
            UltraGridUtil.SetComboUltraGrid(this.grid1, "WORKERCENTCODE", dtTemp, "CODE_ID", "CODE_NAME");

            dtTemp = _Common.Standard_CODE("UNITCODE");     //단위
            UltraGridUtil.SetComboUltraGrid(this.grid1, "UNITCODE", dtTemp, "CODE_ID", "CODE_NAME");

            dtTemp = _Common.Standard_CODE("WHCODE");     //입고 창고
            UltraGridUtil.SetComboUltraGrid(this.grid1, "WHCODE", dtTemp, "CODE_ID", "CODE_NAME");

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
            BizTextBoxManager btbManager = new BizTextBoxManager();
            btbManager.PopUpAdd(txtWokerID, txtWorkerName, "WORKER_MASTER", new object[] { "", "", "", "", "" });
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
                string sStartDate = string.Format("{0:yyyy-MM-dd}", dtpStart.Value);
                string sEndDate = string.Format("{0:yyyy-MM-dd}", dtpEnd.Value);
                string sOrderNo = Convert.ToString(txtOderNo.Text);


                base.DoInquire();
                _GridUtil.Grid_Clear(grid1);


                dtTemp = helper.FillTable("07PP_ActureOutput_S1", CommandType.StoredProcedure
                                    , helper.CreateParameter("PLANTCODE", sPlantCode, DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("WORKCENTERCODE", sWorkcentercode, DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("STARTDATE", sStartDate, DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("ENDDATE", sEndDate, DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("ORDERNO", sOrderNo, DbType.String, ParameterDirection.Input)
                                    );

                this.ClosePrgForm();
                this.grid1.DataSource = dtTemp;
            }
            catch (Exception ex)
            {
                ShowDialog(ex.ToString(), DialogForm.DialogType.OK);
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
            if (grid1.ActiveRow == null) return; // 선택된 행이 없을 경우 종료
            DataRow drRow = ((DataTable)this.grid1.DataSource).NewRow();
            drRow["ITEMCODE"] = Convert.ToString(this.grid1.ActiveRow.Cells["ITEMCODE"].Value);
            drRow["ITEMNAME"] = Convert.ToString(this.grid1.ActiveRow.Cells["ITEMNAME"].Value);
            drRow["CUSTNAME"] = Convert.ToString(this.grid1.ActiveRow.Cells["CUSTNAME"].Value);
            drRow["STOCKQTY"] = Convert.ToString(this.grid1.ActiveRow.Cells["STOCKQTY"].Value);
            drRow["MATLOTNO"] = Convert.ToString(this.grid1.ActiveRow.Cells["MATLOTNO"].Value);
            drRow["UNITCODE"] = Convert.ToString(this.grid1.ActiveRow.Cells["UNITCODE"].Value);

            // 바코드 디자인 선언
            Report_LotBacode repBarCode = new Report_LotBacode();
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
            // 가동 / 비가동 등록
            DBHelper helper = new DBHelper("", true);
            try
            {
                string sStatus = "R";
                if (btnRunStop.Text == "비가동") sStatus = "S";
                helper.ExecuteNoneQuery("07PP_ActureOutput_U1", CommandType.StoredProcedure
                                                                    , helper.CreateParameter("PLANTCODE", "1000", DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("WORKCENTERCODE", Convert.ToString(this.grid1.ActiveRow.Cells["WORKCENTERCODE"].Value), DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("ORDERNO", Convert.ToString(this.grid1.ActiveRow.Cells["ORDERNO"].Value), DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("ITEMCODE", Convert.ToString(this.grid1.ActiveRow.Cells["ITEMCODE"].Value), DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("UNITCODE", Convert.ToString(this.grid1.ActiveRow.Cells["UNITCODE"].Value), DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("STATUS", sStatus, DbType.String, ParameterDirection.Input)
                                                                    );
                if(helper.RSCODE == "S")
                {
                    helper.Commit();
                    ShowDialog("정상적으로 등록 되었습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                    DoInquire();
                }
                else
                {
                    helper.Rollback();
                    ShowDialog("데이터 등록 중 오류가 발생 하였습니다."+ helper.RSMSG, DC00_WinForm.DialogForm.DialogType.OK);
                }
            }
            catch(Exception ex)
            {
                helper.Rollback();
                ShowDialog(ex.ToString());
            }
            finally
            {
                helper.Close();
            }
        }

        private void ultraLabel9_Click(object sender, EventArgs e)
        {

        }

        private void ultraTextEditor3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnWorker_Click(object sender, EventArgs e)
        {
            // 작업자 등록 시작
            if (grid1.Rows.Count == 0) return;
            if (grid1.ActiveRow == null)
            {
                ShowDialog("작업지시를 선택 후 진행하세요.", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }
            string sWorkId = txtWokerID.Text.ToString();
            if (sWorkId == "")
            {
                ShowDialog("작업자를 선택 후 진행하세요.", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }
            string sOrderNo = grid1.ActiveRow.Cells["ORDERNO"].Value.ToString();
            string sWorkcentercode = grid1.ActiveRow.Cells["WORKCENTERCODE"].Value.ToString();

            DBHelper helper = new DBHelper("", true);
            try
            {
                helper.ExecuteNoneQuery("07PP_ActureOutput_I2", CommandType.StoredProcedure,
                                         helper.CreateParameter("PLANTCODE",      "1000",          DbType.String, ParameterDirection.Input),
                                         helper.CreateParameter("WORKER",         sWorkId,         DbType.String, ParameterDirection.Input),
                                         helper.CreateParameter("ORDERNO",        sOrderNo,        DbType.String, ParameterDirection.Input),
                                         helper.CreateParameter("WORKCENTERCODE", sWorkcentercode, DbType.String, ParameterDirection.Input)
                                         );
                if (helper.RSCODE == "S")
                {
                    helper.Commit();
                    ShowDialog(helper.RSMSG, DC00_WinForm.DialogForm.DialogType.OK);
                }
                else
                {
                    helper.Rollback();
                    ShowDialog(helper.RSMSG, DC00_WinForm.DialogForm.DialogType.OK);
                }

                helper.Commit();
            }
            catch (Exception ex)
            {
                helper.Rollback();
            }
            finally
            {
                helper.Close();
            }
        }

        private void btnLotIn_Click(object sender, EventArgs e)
        {
            // LOT 투입
            if (this.grid1.ActiveRow == null) return;
            DBHelper helper = new DBHelper("", true);
            try
            {
                string sItemcode = Convert.ToString(grid1.ActiveRow.Cells["ITEMCODE"].Value);
                string sLotno = Convert.ToString(txtInLotNo.Text);
                string sWorkercenterCode = Convert.ToString(grid1.ActiveRow.Cells["WORKCENTERCODE"].Value);

                string sOrderno = Convert.ToString(grid1.ActiveRow.Cells["ORDERNO"].Value);
                string sUnitCode = Convert.ToString(grid1.ActiveRow.Cells["UNITCODE"].Value);
                string sInFlag = Convert.ToString(btnLotIn.Text);
                string sWorker = Convert.ToString(grid1.ActiveRow.Cells["WORKER"].Value);
                if (sInFlag == "투입")
                {
                    sInFlag = "IN";
                }
                else sInFlag = "OUT";

                helper.ExecuteNoneQuery("07PP_ActureOutput_I1", CommandType.StoredProcedure,
                                        helper.CreateParameter("PLANTCODE", "1000", DbType.String, ParameterDirection.Input),
                                        helper.CreateParameter("ITEMCODE", sItemcode, DbType.String, ParameterDirection.Input),
                                        helper.CreateParameter("LOTNO", sLotno, DbType.String, ParameterDirection.Input),
                                        helper.CreateParameter("WORKCENTERCODE", sWorkercenterCode, DbType.String, ParameterDirection.Input),
                                        helper.CreateParameter("ORDERNO", sOrderno, DbType.String, ParameterDirection.Input),
                                        helper.CreateParameter("UNITCODE", sUnitCode, DbType.String, ParameterDirection.Input),
                                        helper.CreateParameter("INFLAG", sInFlag, DbType.String, ParameterDirection.Input),
                                        helper.CreateParameter("MAKER", sWorker, DbType.String, ParameterDirection.Input)
                                        );
                if (helper.RSCODE == "S")
                {
                    helper.Commit();
                    ShowDialog(helper.RSMSG, DC00_WinForm.DialogForm.DialogType.OK);
                    DoInquire();
                }
                else
                {
                    helper.Rollback();
                    ShowDialog(helper.RSMSG, DC00_WinForm.DialogForm.DialogType.OK);
                }
            }
            catch (Exception ex)
            {
                helper.Rollback();
                ShowDialog(helper.RSMSG, DC00_WinForm.DialogForm.DialogType.OK);
            }
            finally
            {
                helper.Close();
            }
        }

        private void grid1_AfterRowActivate(object sender, EventArgs e)
        {
            if (Convert.ToString(this.grid1.ActiveRow.Cells["WORKSTATUSCODE"].Value) == "R")
            {
                btnRunStop.Text = "비가동";
            }
            else btnRunStop.Text = "가동";

            string sMatLotno = Convert.ToString(grid1.ActiveRow.Cells["MATLOTNO"].Value);
            if(sMatLotno != "")
            {
                txtInLotNo.Text = sMatLotno;
                btnLotIn.Text = "투입취소";
            }
            else
            {
                txtInLotNo.Text = "";
                btnLotIn.Text = "투입";
            }
            txtWokerID.Text = Convert.ToString(grid1.ActiveRow.Cells["WORKER"].Value);
            txtWorkerName.Text = Convert.ToString(grid1.ActiveRow.Cells["WORKERNAME"].Value);
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            // 생산 실적 등록
            if(this.grid1.ActiveRow == null)
            {
                ShowDialog("작업지시를 선택 하세요.", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }
            double dProdQty   = 0; // 누적 양품 수량
            double dErrorQty  = 0; // 누적 불량 수량
            double dTProdQty  = 0; // 입력 양품 수량
            double dTErrorQty = 0; // 입력 불량 수량
            double dOderQty   = 0; // 작업지시 수량
            double dInQty     = 0; // 투입 LOT 잔량
            
            string sProdQty =
                Convert.ToString(this.grid1.ActiveRow.Cells["PRODQTY"].Value).Replace(",", "");
            double.TryParse(sProdQty, out dProdQty);

           string sBadQty =
                Convert.ToString(this.grid1.ActiveRow.Cells["BADQTY"].Value).Replace(",", "");
            double.TryParse(sBadQty, out dErrorQty);

            string sTProdQty = Convert.ToString(txtProduct.Text);
            double.TryParse(sTProdQty, out dTProdQty);

            string sTBadQty = Convert.ToString(txtBad.Text);
            double.TryParse(sTBadQty, out dTErrorQty);

            string sOderQty =
               Convert.ToString(this.grid1.ActiveRow.Cells["PLANQTY"].Value).Replace(",", "");
            double.TryParse(sOderQty, out dOderQty);

            string sInQty =
              Convert.ToString(this.grid1.ActiveRow.Cells["COMPONENTQTY"].Value).Replace(",", "");
            double.TryParse(sInQty, out dInQty);

            if(dInQty == 0)
            {
                ShowDialog("투입한 LOT가 존재하지 않습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }

            if (dTProdQty + dTErrorQty == 0)
            {
                ShowDialog("실적 수량을 입력하세요.", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }

            if (dOderQty < (dProdQty+dErrorQty) + (dTProdQty + dTErrorQty))
            {
                ShowDialog("생산수량 및 불량 수량의 합계가 지시수량보다 많습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }

            DBHelper helper = new DBHelper("", true);
            try
            {
                helper.ExecuteNoneQuery("07PP_ActureOutput_U2", CommandType.StoredProcedure
                                                                   , helper.CreateParameter("PLANTCODE",      "1000", DbType.String, ParameterDirection.Input)
                                                                   , helper.CreateParameter("WORKCENTERCODE", Convert.ToString(this.grid1.ActiveRow.Cells["WORKCENTERCODE"].Value), DbType.String, ParameterDirection.Input)
                                                                   , helper.CreateParameter("ORDERNO",        Convert.ToString(this.grid1.ActiveRow.Cells["ORDERNO"].Value), DbType.String, ParameterDirection.Input)
                                                                   , helper.CreateParameter("ITEMCODE",       Convert.ToString(this.grid1.ActiveRow.Cells["ITEMCODE"].Value), DbType.String, ParameterDirection.Input)
                                                                   , helper.CreateParameter("UNITCODE",       Convert.ToString(this.grid1.ActiveRow.Cells["UNITCODE"].Value), DbType.String, ParameterDirection.Input)
                                                                   , helper.CreateParameter("PRODQTY",        dTProdQty, DbType.String, ParameterDirection.Input)
                                                                   , helper.CreateParameter("ERRORQTY",       dTErrorQty, DbType.String, ParameterDirection.Input)
                                                                   , helper.CreateParameter("MATLOTNO",       Convert.ToString(this.grid1.ActiveRow.Cells["MATLOTNO"].Value), DbType.String, ParameterDirection.Input)
                                                                   , helper.CreateParameter("CITEMCODE",      Convert.ToString(this.grid1.ActiveRow.Cells["COMPONENT"].Value), DbType.String, ParameterDirection.Input)
                                                                   , helper.CreateParameter("CUNITCODE",      Convert.ToString(this.grid1.ActiveRow.Cells["CUNITCODE"].Value), DbType.String, ParameterDirection.Input)
                                                                   );
                if (helper.RSCODE != "S")
                {
                    helper.Rollback();
                    ShowDialog(helper.RSMSG);
                    return;
                }
                helper.Commit();
                ShowDialog("생산실적 등록을 완료 하였습니다.", DialogForm.DialogType.OK);
                DoInquire();
                //txtInLotNo.Text = "";
                txtProduct.Text = "";
                txtBad.Text     = "";

            }
            catch(Exception ex)
            {
                helper.Rollback();
                ShowDialog(ex.ToString(), DC00_WinForm.DialogForm.DialogType.OK);
            }
            finally
            {
                helper.Close();
            }

        }

        private void btnOrderClose_Click(object sender, EventArgs e)
        {
            // 작업지시 종료
            if (grid1.Rows.Count == 0) return;
            if (grid1.ActiveRow == null) return;
            if(Convert.ToString(grid1.ActiveRow.Cells["MATLOTNO"].Value) != "")
            {
                ShowDialog("LOT 투입 취소 후 진행하세요.", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }
            // 가동일 경우 동료 안되도록 확인
            if(Convert.ToString(grid1.ActiveRow.Cells["WORKSTATUSCODE"].Value) != "S")
            {
                ShowDialog("비가동 등록 후 진행하세요.", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }
            DBHelper helper = new DBHelper("", true);
            try
            {
                helper.ExecuteNoneQuery("07PP_ActureOutput_U3", CommandType.StoredProcedure
                                                                    , helper.CreateParameter("PLANTCODE", plantCode, DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("WORKCENTERCODE", Convert.ToString(this.grid1.ActiveRow.Cells["WORKCENTERCODE"].Value), DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("ORDERNO", Convert.ToString(this.grid1.ActiveRow.Cells["ORDERNO"].Value), DbType.String, ParameterDirection.Input)
                                                                    );
                if (helper.RSCODE != "S")
                {
                    helper.Rollback();
                    ShowDialog(helper.RSMSG);
                    return;
                }
                helper.Commit();
                ShowDialog("상태 등록을 완료 하였습니다.", DialogForm.DialogType.OK);
                DoInquire();
                txtInLotNo.Text = "";
                txtProduct.Text = "";
                txtBad.Text = "";
            }
            catch(Exception ex)
            {
                helper.Rollback();
                ShowDialog(ex.ToString(), DC00_WinForm.DialogForm.DialogType.OK);
            }
            finally
            {
                helper.Close();
            }
        }

        private void txtWokerID_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnWorker_Click_1(object sender, EventArgs e)
        {

        }

        private void txtWorkerName_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}


