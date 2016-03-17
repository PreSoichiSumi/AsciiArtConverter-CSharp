using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsciiArtConverter08.Util
{
    static class InfoUtil
    {
        public static string GetInfoAA(Dictionary<string,string> dic)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("****** アスキーアート変換設定 ******\r\n");

            //画像サイズ
            sb.Append("\r\n");
            if (dic["sizeType"] == "0")
            {
                sb.Append("  画像サイズ － 変換しない\r\n");
            }
            else
            {
                sb.Append("  画像サイズ － " + dic["sizeImage_w"] + " x " + dic["sizeImage_h"] + "\r\n");
            }

            //輪郭抽出精度
            sb.Append("\r\n");
            sb.Append("  輪郭抽出精度 － " + dic["accuracy"] + "\r\n");
            sb.Append("  抽出比較範囲 － " + dic["lapRange"] + " x " + dic["lapRange"] + "\r\n");
            sb.Append("  点と点の間が 「" + dic["connectRange"] + "」 以下の場合は、繋がっている線とみなします。\r\n");
            sb.Append("  繋がっている線の長さが 「" + dic["noizeLen"] + "」 以下の場合は、ノイズとして除去します。\r\n");

            //キャラセット
            sb.Append("\r\n");
            if (dic["charSet"] == "0")
            {
                sb.Append("  キャラクタセット － キャラクタセット１（最少）\r\n");
            }
            if (dic["charSet"] == "1")
            {
                sb.Append("  キャラクタセット － キャラクタセット２（少）\r\n");
            }
            if (dic["charSet"] == "2")
            {
                sb.Append("  キャラクタセット － キャラクタセット３（中）\r\n");
            }
            if (dic["charSet"] == "3")
            {
                sb.Append("  キャラクタセット － キャラクタセット４（多）\r\n");
            }
            if (dic["charSet"] == "4")
            {
                sb.Append("  キャラクタセット － キャラクタセット５（最多）\r\n");
            }
            if (dic["charSet"] == "5")
            {
                sb.Append("  キャラクタセット － ユーザ定義セット１\r\n");
            }
            if (dic["charSet"] == "6")
            {
                sb.Append("  キャラクタセット － ユーザ定義セット２\r\n");
            }
            if (dic["charSet"] == "7")
            {
                sb.Append("  キャラクタセット － ユーザ定義セット３\r\n");
            }
            if (dic["charSet"] == "8")
            {
                sb.Append("  キャラクタセット － ユーザ定義セット４\r\n");
            }
            if (dic["charSet"] == "9")
            {
                sb.Append("  キャラクタセット － ユーザ定義セット５\r\n");
            }

            //マッチング範囲
            sb.Append("\r\n");
            if (dic["match"] == "1")
            {
                sb.Append("  マッチング範囲 － 狭\r\n");
            }
            if (dic["match"] == "2")
            {
                sb.Append("  マッチング範囲 － 中\r\n");
            }
            if (dic["match"] == "3")
            {
                sb.Append("  マッチング範囲 － 広\r\n");
            }

            //フォント及び行間
            sb.Append("\r\n");
            sb.Append("  フォント － " + dic["fontName"] + " " + dic["fontSize"] + "px\r\n");
            sb.Append("  行間 － " + dic["pitch"] + "px\r\n");

            //マッチング設定
            sb.Append("\r\n");
            sb.Append("  線の角度を考慮する場合\r\n");
            sb.Append("    " + dic["score1"] + " 以上で採用、 " + dic["score2"] + " 以上で確定\r\n");
            if (dic["angle"] == "1")
            {
                sb.Append("    有効とする角度の範囲 － 狭\r\n");
            }
            else
            {
                sb.Append("    有効とする角度の範囲 － 広\r\n");
            }
            sb.Append("  線の角度を考慮しない場合\r\n");
            if (dic["useNotDir"].ToLower() == "true")
            {
                sb.Append("    有効\r\n");
            }
            else
            {
                sb.Append("    無効\r\n");
            }
            sb.Append("    " + dic["score3"] + " 以上で採用、 " + dic["score4"] + " 以上で確定\r\n");

            //変換処理
            sb.Append("\r\n");
            if (dic["multi"].ToLower() == "true")
            {
                sb.Append("  マルチスレッド － 有効\r\n");
            }
            else
            {
                sb.Append("  マルチスレッド － 無効\r\n");
            }

            //マッチング回数
            sb.Append("\r\n");
            if (dic["matchCnt"] == "0")
            {
                sb.Append("  マッチング回数 － １回 （補正なし）\r\n");
            }
            if (dic["matchCnt"] == "1")
            {
                sb.Append("  マッチング回数 － ２回 （ピリオドの補正）\r\n");
            }
            if (dic["matchCnt"] == "2")
            {
                sb.Append("  マッチング回数 － ３回 （ピリオドと半角空白の補正）\r\n");
            }

            //トーン設定
            sb.Append("\r\n");
            if (dic["tone"].ToLower() == "true")
            {
                sb.Append("  トーン設定 － 有効\r\n");
            }
            else
            {
                sb.Append("  トーン設定 － 無効\r\n");
            }
            if (dic["reversal"].ToLower() == "true")
            {
                sb.Append("  トーン明暗 － 反転する\r\n");
            }
            else
            {
                sb.Append("  トーン明暗 － 反転しない\r\n");
            }
            sb.Append("  トーン対象輝度 － " + dic["toneValue"] + "\r\n");
            sb.Append("  トーン変換文字 － " + dic["toneTxt"] + "\r\n");

            //文字色背景色
            sb.Append("\r\n");
            sb.Append("  文字色（R,G,B） － " + dic["textColor"] + "\r\n");
            sb.Append("  背景色（R,G,B） － " + dic["canvsColor"] + "\r\n");

            return sb.ToString();

        }

        public static string GetInfoProject(Dictionary<string, string> dic)
        {
            StringBuilder sb = new StringBuilder();

            //プロジェクト
            if (dic["isProject"].ToLower() == "true")
            {
                sb.Append("\r\n");
                sb.Append("\r\n");
                sb.Append("****** プロジェクト変換設定 ******\r\n\r\n");
                sb.Append("  プロジェクトファイル － " + dic["proj_filename"] + "\r\n");
                sb.Append("\r\n");

                sb.Append("  プロジェクトモード － " + dic["proj_mode"] + "\r\n");

                if (dic["proj_line"].ToLower() == "true")
                {
                    sb.Append("  輪郭線の画像も保存する\r\n");
                }
                else
                {
                    sb.Append("  輪郭線の画像は保存しない\r\n");
                }
                sb.Append("  アスキーアートの文字コード － " + dic["proj_enc"]);
                sb.Append("\r\n");

                if (dic["proj_mk"].ToLower() == "true")
                {
                    sb.Append("  変換終了後に動画を作成する\r\n");
                }
                else
                {
                    sb.Append("  変換終了後に動画を作成しない\r\n");
                }

                sb.Append("\r\n");

                if (dic["proj_bitrate"] == "0")
                {
                    sb.Append("  圧縮ビットレート － 無圧縮\r\n");
                }
                else
                {
                    sb.Append("  圧縮ビットレート － " + Convert.ToInt32(dic["proj_bitrate"]).ToString("#,###") + "k\r\n");
                }

                sb.Append("\r\n");
                sb.Append("  FPS - " + dic["proj_fps"] + "（" + dic["proj_skip"] + "フレームごと）\r\n");

            }

            return sb.ToString();
        }
    }
}
