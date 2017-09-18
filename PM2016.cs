using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;

using System.Windows.Forms;

using System.Diagnostics; // Class Process
using System.Collections; // Class IComparer
using System.Threading; //   Threading



namespace PM2016
{
    public partial class PM2016 : Form
    {
        private Thread ShowThread; //프로세스 목록 스레드
        private Thread CpuThread; //Cpu 사용량 스레드
        private Thread MemThread; //Mem 사용량 스레드

        float usingMem = 0;

        private delegate void ProcessUpdateDelegate(); //목록 업데이트 델리게이트
        private ProcessUpdateDelegate procDelg = null;
        /*전역 변수 선언*/
        private int procCounter = 0;

        public PM2016()
        {
            InitializeComponent();
        }

        private void PM2016_Load(object sender, EventArgs e)
        {
            ProcessView(); // 초기 프로세스 목록 출력

            ShowThread = new Thread(ProcessUpdate); // ProcessUpdate 실행 쓰레드생성
            ShowThread.Start(); //ShowThread 시작
            CpuThread = new Thread(GetCpuUsage);
            CpuThread.Start();
            MemThread = new Thread(GetMemUsage);
            MemThread.Start();

            procDelg = new ProcessUpdateDelegate(ProcessView);
        }

        /*Process 목록 출력*/
        /*Main Thread*/
        private void ProcessView()
        {
            /*프로세스 목록 출력*/
            /*****************************************************************/
            try
            {
                this.listView.Items.Clear(); // 리스트 뷰 '항목' 초기화
                procCounter = 0; //프로세스 초기 갯수 초기화
                usingMem = 0; //사용중 메모리 양 초기화

                foreach (var proc in Process.GetProcesses())
                {
                    long currentMem = proc.WorkingSet64;
                    usingMem += currentMem;

                    var strArray = new string[]
                    {
                        proc.ProcessName.ToString(),
                        proc.Id.ToString(),
                        MemoryString(currentMem),

                    };

                    var lvt = new ListViewItem(strArray);
                    this.listView.Items.Add(lvt);
                    procCounter++;

                }
                usingMem /= (1024 * 1024); // MB로 변환

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            /*****************************************************************/

            this.toolProcess.Text = "프로세스 : " + procCounter.ToString() + "개";

        }

        /*Process 목록 업데이트(지속)*/
        /*Show Thread*/
        private void ProcessUpdate()
        {
            try
            {
                while (true) //프로그램 종료시까지 반복
                {
                    Thread.Sleep(10000); //10초마다 갱신(계속 갱신할경우 무리)
                    Invoke(procDelg);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /*Cpu 사용량 Thread*/
        public void GetCpuUsage()
        {
            while (true)
            {
                var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                cpuCounter.NextValue();
                Thread.Sleep(1000);
                this.toolCPU.Text = "CPU 사용률 : " + Convert.ToString(String.Format("{0:f2}", cpuCounter.NextValue()) + "%");
            }
        }
        /*메모리 사용량 Thread*/
        public void GetMemUsage()
        {
            while (true)
            {
                var memCounter = new PerformanceCounter("Memory", "Available MBytes");
                float usedValue;
                usedValue = memCounter.NextValue();
                this.toolMem.Text = "메모리 사용률 : " + Convert.ToString(String.Format("{0:f2}", usingMem*100 / (usingMem + usedValue)) + "%");
                Thread.Sleep(1000);
            }
        }

        private void btnKill_Click(object sender, EventArgs e)
        {
            ProcessKill();
        }

        private void ProcessKill()
        {
            try
            {
                var PID =   //선택된 리스트 항목의 PID를 구함
                    Convert.ToInt32(this.listView.SelectedItems[0].SubItems[1].Text);

                Process targetProcess = Process.GetProcessById(PID); //PID로 프로세스 탐색

                if (!(targetProcess == null))
                {
                    var message =
                        MessageBox.Show
                        (
                            this.listView.SelectedItems[0].SubItems[0].Text +
                            " 프로세스를 종료합니까?", "알림",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning
                        );

                    if (message == DialogResult.Yes)
                    {
                        targetProcess.Kill();
                        ProcessView();

                    }
                }
                else
                {
                    MessageBox.Show
                    (
                        this.listView.SelectedItems[0].SubItems[0].Text +
                        " 프로세스가 존재하지 않습니다.", "알림",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ProcessView();
            }


        }

        private void listView_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {


                //오른쪽 클릭일 경우
                if (e.Button.Equals(MouseButtons.Right))
                {
                    var PID =   //선택된 리스트 항목의 PID를 구함
                    Convert.ToInt32(this.listView.SelectedItems[0].SubItems[1].Text);

                    Process targetProcess = Process.GetProcessById(PID); //PID로 프로세스 탐색

                    //오른쪽 메뉴를 만듭니다
                    ContextMenu rightMenu = new ContextMenu();


                    RadioButton a = new RadioButton();
                    RadioButton b = new RadioButton();

                    //메뉴에 들어갈 아이템을 만듭니다
                    MenuItem mAf = new MenuItem("Set Affinity");
                    MenuItem mPr = new MenuItem("Set Priority");


                    /*Affinity Part Start*/
                    /********************************************************************************/
                    Console.WriteLine((int)targetProcess.ProcessorAffinity);
                    mAf.Click += (senders, es) =>
                    {
                        AffinityForm aForm = new AffinityForm(targetProcess.ProcessorAffinity);
                        if (aForm.ShowDialog() == DialogResult.OK)
                        {
                            targetProcess.ProcessorAffinity = aForm.AffinityNum;
                            Console.WriteLine((int)targetProcess.ProcessorAffinity);
                        }
                    };
                    /********************************************************************************/
                    /*Affinity  Part Start*/


                    /*Priority Part Start*/
                    /********************************************************************************/
                    //하위메뉴들 선언
                    MenuItem prReti = new MenuItem("RealTime");
                    MenuItem prHigh = new MenuItem("High");
                    MenuItem prAbno = new MenuItem("AboveNormal");
                    MenuItem prNorm = new MenuItem("Normal");
                    MenuItem prBeno = new MenuItem("BelowNormal");
                    MenuItem prIdle = new MenuItem("Idle");
                    //하위메뉴들 추가
                    mPr.MenuItems.Add(prReti);
                    mPr.MenuItems.Add(prHigh);
                    mPr.MenuItems.Add(prAbno);
                    mPr.MenuItems.Add(prNorm);
                    mPr.MenuItems.Add(prBeno);
                    mPr.MenuItems.Add(prIdle);
                    //라디오버튼 활성화
                    prReti.RadioCheck = true;
                    prHigh.RadioCheck = true;
                    prAbno.RadioCheck = true;
                    prNorm.RadioCheck = true;
                    prBeno.RadioCheck = true;
                    prIdle.RadioCheck = true;

                    switch (targetProcess.PriorityClass)
                    {
                        case ProcessPriorityClass.RealTime:
                            {
                                prReti.Checked = true;
                                break;
                            }
                        case ProcessPriorityClass.High:
                            {
                                prHigh.Checked = true;
                                break;
                            }
                        case ProcessPriorityClass.AboveNormal:
                            {
                                prAbno.Checked = true;
                                break;
                            }
                        case ProcessPriorityClass.Normal:
                            {
                                prNorm.Checked = true;
                                break;
                            }
                        case ProcessPriorityClass.BelowNormal:
                            {
                                prBeno.Checked = true;
                                break;
                            }
                        case ProcessPriorityClass.Idle:
                            {
                                prIdle.Checked = true;
                                break;
                            }
                    }
                    prReti.Click += (senders, es) =>
                    {
                        targetProcess.PriorityClass = ProcessPriorityClass.RealTime;
                    };
                    prHigh.Click += (senders, es) =>
                    {
                        targetProcess.PriorityClass = ProcessPriorityClass.High;
                    };
                    prAbno.Click += (senders, es) =>
                    {
                        targetProcess.PriorityClass = ProcessPriorityClass.AboveNormal;
                    };
                    prNorm.Click += (senders, es) =>
                    {
                        targetProcess.PriorityClass = ProcessPriorityClass.Normal;
                    };
                    prBeno.Click += (senders, es) =>
                    {
                        targetProcess.PriorityClass = ProcessPriorityClass.BelowNormal;
                    };
                    prIdle.Click += (senders, es) =>
                    {
                        targetProcess.PriorityClass = ProcessPriorityClass.Idle;
                    };
                    /********************************************************************************/
                    /*Priority Part End*/

                    //메뉴에 메뉴 아이템을 등록해줍니다
                    rightMenu.MenuItems.Add(mAf);
                    rightMenu.MenuItems.Add(mPr);

                    //현재 마우스가 위치한 장소에 메뉴를 띄워줍니다
                    rightMenu.Show(this.listView, new Point(e.X, e.Y));
                }

            }
            catch (Exception ex)
            {
                /*시스템 프로세스 권한문제 발생가능(액세스가 거부되었습니다.)*/
                Console.WriteLine(ex.Message);
            }
        }

        /*process 메모리를 나타낼 함수*/
        private string MemoryString(long MemoryNum)
        {
            Double memNum;
            memNum = Convert.ToDouble(MemoryNum);
            memNum = memNum / (1024 * 1024);
            return String.Format("{0:N}", memNum) + "MB";
        }


    }
}
