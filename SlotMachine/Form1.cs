using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SlotMachine
{


    public partial class Form1 : Form
    {
        //ゲームモード
        private Boolean game_mode = false;

        //デバックモード
        private const Boolean debug_mode = false;


        //スピンモード 真：回転中 偽：ストップ
        private Boolean spin1_mode = false;
        private Boolean spin2_mode = false;
        private Boolean spin3_mode = false;

        //役判定 真：判定する 偽：しない
        //private Boolean role_mode = false;

        //各インデックス
        private int reelIndex1;
        private int reelIndex2;
        private int reelIndex3;
        //各シンボルインデックス
        private int symbolIndex1;
        private int symbolIndex2;
        private int symbolIndex3;

        //所持金
        private int totalMoney = 1000;

        //1ゲームの掛け金
        public const int ONE_GAME_MONEY = 10;

        
        //リールシンボル
        private string[] reelSymbols = {
            "Default.png" ,
            "Cherry.png" ,
            "Plain.png" ,
            "Watermelon.png" ,
            "Bell.png" ,
            "Bar.png" ,
            "Seven.png" ,
        };


        public Form1()
        {
            InitializeComponent();

            lbResult.Text = "";
            lbTotalMoney.Text = totalMoney.ToString();

            //1bit表記
            lbOnebit.Text = $"1Bit {ONE_GAME_MONEY}円";

            InitializeReel(); //リール初期化処理
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // スロットマシーンのUIを初期化
            // PictureBoxの初期画像を設定（例：空のリール）
            pictureBoxReels1.Image = Properties.Resources.Default;
        }

        /// <summary>
        /// リール初期化処理
        /// </summary>
        private void InitializeReel()
        {
            //currentSymbolIndex = 0;
            reelIndex1 = 0;
            reelIndex2 = 0;
            reelIndex3 = 0;

            reelTimer1 = new Timer();
            reelTimer1.Interval = 50;

            reelTimer2 = new Timer();
            reelTimer2.Interval = 50;

            reelTimer3 = new Timer();
            reelTimer3.Interval = 50;

            //タイマーイベントハンドラ
            reelTimer1.Tick += reelTimer1_Tick;
            reelTimer2.Tick += reelTimer2_Tick;
            reelTimer3.Tick += reelTimer3_Tick;
        }


        private void btSpin1_Click(object sender, EventArgs e)
        {

            //ゲームモードがOFF時は返す
            if (!game_mode) return;

            //自身のスピンモードOFF
            spin1_mode = false;

            //スピン無効化
            btSpin1.Enabled = false;

            //タイマーSTOP
            reelTimer1.Stop();

            //スピンモード判定→役計算
            if( SpinDecision(spin1_mode, spin2_mode, spin3_mode) )
            {
                //役判定
                RoleCalculation(symbolIndex1, symbolIndex2, symbolIndex3);
            }

        }

        private void btSpin2_Click(object sender, EventArgs e)
        {
            if (!game_mode) return;

            //スピンボタン無効化
            btSpin2.Enabled = false;

            //自身のスピンモードOFF
            spin2_mode = false;

            //タイマーSTOP
            reelTimer2.Stop();

            //スピンモード判定→役計算
            if (SpinDecision(spin1_mode, spin2_mode, spin3_mode))
            {
                //役判定
                RoleCalculation(symbolIndex1, symbolIndex2, symbolIndex3);

            }
        }

        private void btSpin3_Click(object sender, EventArgs e)
        {
            if (!game_mode) return;

            //スピン無効化
            btSpin3.Enabled = false;

            //自身のスピンモードOFF
            spin3_mode = false;

            //タイマーSTOP
            reelTimer3.Stop();

            //スピンモード判定→役計算
            if (SpinDecision(spin1_mode, spin2_mode, spin3_mode))
            {
                //役判定
                RoleCalculation(symbolIndex1, symbolIndex2, symbolIndex3);
            }

        }

        private void btStart_Click(object sender, EventArgs e)
        {
            //ゲームモードON
            game_mode = true;

            //スピンボタン有効化
            btSpin1.Enabled = true;
            btSpin2.Enabled = true;
            btSpin3.Enabled = true;

            //各スピンモードON
            spin1_mode = true;
            spin2_mode = true;
            spin3_mode = true;

            //タイマーSTART
            reelTimer1.Start();
            reelTimer2.Start();
            reelTimer3.Start();

            //自身を押せないようにする
            btStart.Enabled = false;

            //テキスト変更
            btStart.Text = "回転中…";
            lbResult.Text = "(;ﾟдﾟ)ｺﾞｸﾘ";

        }

        private void reelTimer1_Tick(object sender, EventArgs e)
        {
            //リール処理
            ReelRotationProcess1(pictureBoxReels1);
        }

        private void reelTimer2_Tick(object sender, EventArgs e)
        {
            ReelRotationProcess2(pictureBoxReels2);
        }

        private void reelTimer3_Tick(object sender, EventArgs e)
        {
            ReelRotationProcess3(pictureBoxReels3);
        }


        /// <summary>
        /// 役計算
        /// </summary>
        /// <param name="sIndex1"></param>
        /// <param name="sIndex2"></param>
        /// <param name="sIndex3"></param>
        private void RoleCalculation(int sIndex1, int sIndex2, int sIndex3)
        {

            // DEBUG : 役の切り替え
            if (debug_mode)
            {
                sIndex1 = 6;
                sIndex2 = 6;
                sIndex3 = 6;
            }

            //各シンボルを比較する
            if (sIndex1 == sIndex2 && sIndex1 == sIndex3)
            {
                //totalMoney += WIN_MONEY;
                int winMoney = 0;

                //役判定
                switch (sIndex1)
                {
                    case (int)Symbol.Cherry:
                        winMoney = ONE_GAME_MONEY * 2;
                        totalMoney += winMoney;
                        break;
                    case (int)Symbol.Bell:
                        winMoney = ONE_GAME_MONEY * 3;
                        totalMoney += winMoney;
                        break;
                    case (int)Symbol.Plain:
                        winMoney = ONE_GAME_MONEY * 4;
                        totalMoney += winMoney;
                        break;
                    case (int)Symbol.Watermelon:
                        winMoney = ONE_GAME_MONEY * 5;
                        totalMoney += winMoney;
                        break;
                    case (int)Symbol.BAR:
                        winMoney = ONE_GAME_MONEY * 10;
                        totalMoney += winMoney;
                        break;
                    case (int)Symbol.Seven:
                        winMoney = ONE_GAME_MONEY * 20;
                        totalMoney += winMoney;
                        break;
                    default:
                        throw new ArgumentException("役の判定に失敗しました。");
                }

                lbResult.Text = $"{winMoney}円Get! おめでとう！:D";

            }
            else
            {
                lbResult.Text = "ざんねんでした(＾ｗ＾)";
                totalMoney -= ONE_GAME_MONEY;
            }


            //所持金１Bit以下の場合ゲームオーバー
            if(totalMoney <= ONE_GAME_MONEY)
            {
                //lbTotalMoney.Text = "0";
                //テキスト変更
                btStart.Text = "お金がもうないです。(´；ω；｀)";
            }
            else
            {
                //自身を押せるようにする
                btStart.Enabled = true;
                //テキスト変更
                btStart.Text = "START!!";
            }

            //所持金画面反映
            lbTotalMoney.Text = totalMoney.ToString();

            //ゲームモードOFF
            game_mode = false;

        }

        /// <summary>
        /// スピン判定処理
        /// </summary>
        /// <param name="spin1"></param>
        /// <param name="spin2"></param>
        /// <param name="spin3"></param>
        private Boolean SpinDecision(bool spin1, bool spin2, bool spin3) {

            //全てのスピンモードがOFFの時、真を返す
            if (!spin1 && !spin2 && !spin3) return true;

            return false;

        }

        
        /// <summary>
        /// リール回転処理1
        /// </summary>
        /// <param name="pb"></param>
        private void ReelRotationProcess1(PictureBox pb)
        {

            // リールの回転処理
            reelIndex1 = (reelIndex1 + 1) % Reel.arrayReel1.Length;
            
            //0のときは+1する（回転ズレ防止）
            if (reelIndex1 == 0) reelIndex1++;

            //シンボル種類を取得→INT変換
            symbolIndex1 = (int)Reel.arrayReel1[reelIndex1];

            //リソースのイメージ取得
            pb.Image = GetSymbolImage(reelSymbols[symbolIndex1]);

        }

        /// <summary>
        /// リール回転処理2
        /// </summary>
        /// <param name="pb"></param>
        private void ReelRotationProcess2(PictureBox pb)
        {

            // リールの回転処理
            reelIndex2 = (reelIndex2 + 1) % Reel.arrayReel2.Length;

            //0のときは+1する（回転ズレ防止）
            if (reelIndex2 == 0) reelIndex2++;

            //シンボル種類を取得→INT変換
            symbolIndex2 = (int)Reel.arrayReel2[reelIndex2];

            //リソースのイメージ取得
            pb.Image = GetSymbolImage(reelSymbols[symbolIndex2]);

        }

        /// <summary>
        /// リール回転処理3
        /// </summary>
        /// <param name="pb"></param>
        private void ReelRotationProcess3(PictureBox pb)
        {

            // リールの回転処理
            reelIndex3 = (reelIndex3 + 1) % Reel.arrayReel3.Length;

            //0のときは+1する（回転ズレ防止）
            if (reelIndex3 == 0) reelIndex3++;

            //シンボル種類を取得→INT変換
            symbolIndex3 = (int)Reel.arrayReel3[reelIndex3];

            //リソースのイメージ取得
            pb.Image = GetSymbolImage(reelSymbols[symbolIndex3]);

        }

        /// <summary>
        /// ファイル名に基づいたリソースを返す。
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        private Image GetSymbolImage(string symbol)
        {

            //拡張子を除去
            symbol = symbol.Replace(".png", "");

            //小文字変換→リソースを切り替えて出力
            switch (symbol.ToLower())
            {
                case "cherry":
                    return Properties.Resources.Cherry;
                case "plain":
                    return Properties.Resources.Plain;
                case "watermelon":
                    return Properties.Resources.Watermelon;
                case "bell":
                    return Properties.Resources.Bell;
                case "bar":
                    return Properties.Resources.Bar;
                case "seven":
                    return Properties.Resources.Seven;
                default:
                    throw new ArgumentException("リソース取得に失敗しました: " + symbol);
            }

        }

        private void verToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // ダイアログボックスを表示
            MessageBox.Show("スロットマシーン Ver0.21\n ©しろうず","バージョン情報");
        }
    }

}
