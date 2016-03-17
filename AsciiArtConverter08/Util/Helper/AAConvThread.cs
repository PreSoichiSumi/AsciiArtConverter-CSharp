using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsciiArtConverter08.Manager;
using AsciiArtConverter08.Manager.Data;
using System.Drawing;
using AsciiArtConverter08.Patturn;
using System.Diagnostics;

namespace AsciiArtConverter08.Util.Helper
{
    public class AAConvThread
    {
        private ConfigManager cm = null;
        private CharManager charm = null;
        private char[,] table = null;
        private char[,] toneTable = null;

        private string aa = "";
        private string aatype = "";

        private object lockObject = new object();

        public AAConvThread(ConfigManager cm, CharManager charm, char[,] table,char[,] toneTable)
        {
            this.cm = cm;
            this.charm = charm;
            this.table = table;
            this.toneTable = toneTable;
        }

        public string AA
        {
            get
            {
                lock (lockObject)
                {
                    return aa;
                }
            }
        }

        public string AA_Type
        {
            get
            {
                lock (lockObject)
                {
                    return aatype;
                }
            }
        }

        public class CharInfo
        {
            private char c;
            private int type;

            public CharInfo(char c, int type)
            {
                this.c = c;
                this.type = type;
            }

            public char Character
            {
                set
                {
                    this.c = value;
                }
                get
                {
                    return this.c;
                }
            }

            public int MatchType
            {
                set
                {
                    this.type = value;
                }
                get
                {
                    return this.type;
                }
            }
        }

        public void DoThread()
        {
            int left = 0;
            int width = this.table.GetLength(0);

            StringBuilder sbAA = new StringBuilder();
            StringBuilder sbAAType = new StringBuilder();

            List<CharInfo> cinfo = new List<CharInfo>();
            int toneIndex = -1;
            int toneCharIndex = 0;
            CharData halfW = this.charm[' '];
            CharData wideW = this.charm['　'];
            CharData priod = this.charm['.'];

            bool left0Flg = true;

            while (left < width)
            {
                CharData append = null;
                CharData match = null;

                //トーン処理
                if (this.toneTable.GetLength(0) > 0)
                {
                    int t = IsTone(left);
                    if (t != -1)
                    {
                        if (t != toneIndex)
                        {
                            toneCharIndex = 0;
                            toneIndex = t;
                        }

                        match = this.charm[this.cm.ToneText[t][toneCharIndex % this.cm.ToneText[t].Length]];
                        cinfo.Add(new CharInfo(match.Character, 0));
                        toneCharIndex++;

                        goto ADD_CHAR;
                    }
                }


                //マッチング処理

                object[] o = GetDirChar(left, ref append);

                int type = 0;

                match = (CharData)o[0];

                if (((double)o[1]) > 0.1d)
                {
                }
                else if (this.cm.UseNotDir)
                {
                    //上記の角度を考慮したマッチングでNG、さらに次もNGだったら角度を考慮しない変換が有効
                    if ((double)(GetDirChar(left+priod.Width, ref append)[1]) <= 0.1)
                    {
                        o = GetChar(left, ref append);
                        match = (CharData)o[0];
                        type = 1;
                    }
                }

                if (append != null && append.Character == ' ' && match.Character == ' ')
                {
                    append = null;
                }

                if (append != null && append.Character == '.' && match.Character == ' ')
                {
                    append = null;
                }

                if (append != null)
                {
                    cinfo.Add(new CharInfo(append.Character, type));
                }

                if (match.Character == '｜' && this.cm.Font.Name == "ＭＳ Ｐゴシック")
                {
                    cinfo.Add(new CharInfo(' ',type));
                    cinfo.Add(new CharInfo('|', type));
                    cinfo.Add(new CharInfo(' ', type));
                }
                else
                {
                    cinfo.Add(new CharInfo(match.Character, type));
                }



            ADD_CHAR:

                //マッチした文字を追加

                for (int i = 0; i < cinfo.Count; i++)
                {
                    //char c = cinfo[i].Character;

                    //if (i > 0 && c == ' ' && cinfo[i - 1].Character == ' ')
                    //{
                    //    cinfo[i - 1].Character = '　';
                    //    cinfo.RemoveAt(i);
                    //    continue;
                    //}

                    //if (left == 0 && i == 0 && cinfo.Count > 1 && c == ' ' && cinfo[i + 1].Character != ' ')
                    //{
                    //    cinfo.Insert(0, new CharInfo('.', 0));
                    //    continue;
                    //}
                }

                for (int i = 0; i < cinfo.Count; i++)
                {
                    char c = cinfo[i].Character;
                    int mtype = cinfo[i].MatchType;

                    if (c == ' ' && sbAA.Length > 0 && sbAA[sbAA.Length - 1] == ' ')
                    {
                        sbAA[sbAA.Length - 1] = '　';
                        left += this.charm['　'].Width - this.charm[c].Width;
                        continue;
                    }

                    sbAA.Append(c);
                    sbAAType.Append(mtype);

                    left += this.charm[c].Width;
                }

                if (left0Flg && sbAA.Length > 3 && sbAA[0] == ' ')
                {
                    sbAA.Remove(0, sbAA.Length);
                    sbAAType.Remove(0, sbAAType.Length);

                    sbAA.Append('.');
                    sbAAType.Append('0');

                    left0Flg = false;

                    left = priod.Width;
                }

                cinfo.Clear();

            }
  
            lock (lockObject)
            {
                this.aa = sbAA.ToString();
                this.aatype = sbAAType.ToString();
            }
        }

        //public void DoThread()
        //{
        //    int left = 0;
        //    int width = this.table.GetLength(0);

        //    StringBuilder sb = new StringBuilder();
        //    char backChar = '\0';
        //    int toneIndex = -1;
        //    int toneCharIndex = 0;
        //    CharData halfW = this.charm[' '];
        //    CharData wideW = this.charm['　'];
        //    CharData priod = this.charm['.'];


        //    while (left < width)
        //    {
        //        CharData append = null;
        //        CharData match = null;

        //        //トーン処理
        //        if (this.toneTable.GetLength(0) > 0)
        //        {
        //            int t = IsTone(left);
        //            if (t != -1)
        //            {
        //                if (t != toneIndex)
        //                {
        //                    toneCharIndex = 0;
        //                    toneIndex = t;
        //                }

        //                match = this.charm[this.cm.ToneText[t][toneCharIndex % this.cm.ToneText[t].Length]];
        //                toneCharIndex++;

        //                goto ADD_CHAR;
        //            }
        //        }


        //        //マッチング処理

        //        object[] o = GetDirChar(left, ref append);

        //        if (((double)o[1]) > 0.1d)
        //        {
        //            match = (CharData)o[0];
        //        }
        //        else
        //        {
        //            o = GetChar(left, ref append);
        //            match = (CharData)o[0];
        //        }

        //        if (append == null)
        //        {
        //            if (backChar == ' ' && match.Character == ' ')
        //            {
        //                left = left - halfW.Width;
        //                match = wideW;
        //                backChar = '\0';
        //            }
        //        }
        //        else if (append.Character == ' ')
        //        {
        //            if (backChar == ' ')
        //            {
        //                left = left - halfW.Width;
        //                match = wideW;
        //                backChar = '\0';
        //                append = null;
        //            }
        //        }


        //    ADD_CHAR:

        //        //マッチした文字を追加

        //        if (backChar != '\0')
        //        {
        //            sb.Append(backChar);
        //        }

        //        if (append != null)
        //        {
        //            sb.Append(append.Character);
        //            left += append.Width;
        //        }

        //        backChar = match.Character;
        //        left += match.Width;

        //        if (sb.Length == 1 && sb[0] == ' ')
        //        {
        //            sb.Remove(0, sb.Length);
        //            left = priod.Width;
        //            backChar = '.';
        //            continue;
        //        }
        //    }

        //    sb.Append(backChar);

        //    lock (lockObject)
        //    {
        //        this.aa = sb.ToString();
        //    }
        //}

        private int IsTone(int left)
        {
            char[,] baseTable = AAUtil.TrimTable(this.toneTable, 0, left, this.table.GetLength(1), this.charm[0].Width, 0);

            //int cnt = 0;
            for (int y = 0; y < baseTable.GetLength(1); y++)
            {
                for (int x = 0; x < baseTable.GetLength(0); x++)
                {
                    if (baseTable[x, y] == ' ' || baseTable[x, y] == '□')
                    {
                        return -1;
                    }
                }
            }

            return baseTable[baseTable.GetLength(0) / 2, baseTable.GetLength(1) / 2] - '0';
        }

        private object[] GetDirChar(int left, ref CharData cd)
        {
            object[] o1 = null;
            object[] o2 = null;
            object[] o3 = null;

            o1 = GetDirMatchData(left, '\0');

            if ((double)o1[1] >= this.cm.Score2 || this.cm.MatchCount == 0)
            {
                cd = null;
                return o1;
            }

            o2 = GetDirMatchData(left + this.charm['.'].Width, '.');

            if ((double)o2[1] >= this.cm.Score2)
            {
                cd = this.charm['.'];
                return o2;
            }

            if (this.cm.MatchCount == 1)
            {
                if ((double)o1[1] >= (double)o2[1])
                {
                    cd = null;
                    return o1;
                }
                else
                {
                    cd = this.charm['.'];
                    return o2;
                }

            }

            o3 = GetDirMatchData(left + this.charm[' '].Width, ' ');

            if ((double)o3[1] >= this.cm.Score2)
            {
                cd = this.charm[' '];
                return o3;
            }

            double d1 = (double)o1[1];
            double d2 = (double)o2[1];
            double d3 = (double)o3[1];

            if (d1 >= d2 && d1 >= d3)
            {
                cd = null;
                return o1;
            }

            if (d2 >= d1 && d2 >= d3)
            {
                cd = this.charm['.'];
                return o2;
            }

            cd = this.charm[' '];
            return o3;

        }

        private object[] GetChar(int left, ref CharData cd)
        {
            object[] o1 = null;
            object[] o2 = null;
            object[] o3 = null;

            o1 = GetMatchData(left, '\0');

            if ((double)o1[1] >= this.cm.Score4 || this.cm.MatchCount == 0)
            {
                cd = null;
                return o1;
            }

            o2 = GetMatchData(left + this.charm['.'].Width, '.');

            if ((double)o2[1] >= this.cm.Score4)
            {
                cd = this.charm['.'];
                return o2;
            }

            if (this.cm.MatchCount == 1)
            {
                if ((double)o1[1] >= (double)o2[1])
                {
                    cd = null;
                    return o1;
                }
                else
                {
                    cd = this.charm['.'];
                    return o2;
                }

            }

            o3 = GetMatchData(left + this.charm[' '].Width, ' ');

            if ((double)o3[1] >= this.cm.Score4)
            {
                cd = this.charm[' '];
                return o3;
            }

            double d1 = (double)o1[1];
            double d2 = (double)o2[1];
            double d3 = (double)o3[1];

            if (d1 >= d2 && d1 >= d3)
            {
                cd = null;
                return o1;
            }

            if (d2 >= d1 && d2 >= d3)
            {
                cd = this.charm['.'];
                return o2;
            }

            cd = this.charm[' '];
            return o3;

        }

        public object[] GetDirMatchData(int left,char addc)
        {
            CharData matchData = null;
            CharData nowData = null;
            char[,] baseTable = null;
            char[,] baseDirTable = null;
            char[,] dirTable = null;

            int pointCount = 0;

            int totalPointCount = 0;
            double score = 0.0d;
            HashSet<Point> hitCharPoint = new HashSet<Point>();
            int hitTable = 0;

            double maxScore = double.MinValue;

            baseTable = AAUtil.TrimTable(this.table, 0, left, this.table.GetLength(1), this.charm[this.charm.Count - 1].Width, 1);

            PatturnBuilder pb = new PatturnBuilder(baseTable);
            baseDirTable = pb.Patturn;
            

            baseDirTable = new PatturnBuilder(baseTable).Patturn;

            for (int i = 0; i < this.charm.Count; i++)
            {
                nowData = this.charm[i];

                //AAUtil.DebugTable(nowData.Patturn);

                //テーブル初期化
                if (i == 0 || this.charm[i - 1].Width != nowData.Width)
                {
                    //baseTable = AAUtil.TrimTable(this.table, 0, left, this.table.GetLength(1), nowData.Width, 1);

                    dirTable = AAUtil.TrimTable(baseDirTable, 0, 0, nowData.Height, nowData.Width, 0);

                    pointCount = 0;

                    for (int y = 0; y < dirTable.GetLength(1); y++)
                    {
                        for (int x = 0; x < dirTable.GetLength(0); x++)
                        {
                            if (dirTable[x, y] != ' ' && dirTable[x, y] != '0')
                            {
                                pointCount++;
                            }
                        }
                    }
                    
                }

                //if (nowData.Character == '｜')
                //{
                //    AAUtil.DebugTable(this.table);
                //    AAUtil.DebugTable(baseTable);
                //    AAUtil.DebugTable(baseDirTable);
                //    AAUtil.DebugTable(dirTable);
                //    AAUtil.DebugTable(nowData.Patturn);
                //    AAUtil.DebugTable(nowData.NormalPatturn);
                //}

                if (i == 0 && pointCount <= 2)
                {
                    return new object[] { this.charm[' '], 0.0d };
                }

                if (nowData.IsTarget == false || pointCount <= 2)
                {
                    continue;
                }

                if (nowData.Character == '｜'  && addc=='.')
                {
                    continue;
                }

                if (nowData.IsHorizonLine && addc != '\0')
                {
                    continue;
                }

                //if (nowData.Character == '|' || nowData.Character == '｜')
                //{
                //    AAUtil.DebugTable(this.table);
                //    AAUtil.DebugTable(baseTable);
                //    AAUtil.DebugTable(baseDirTable);
                //    AAUtil.DebugTable(dirTable);
                //    AAUtil.DebugTable(nowData.Patturn);
                //    AAUtil.DebugTable(nowData.NormalPatturn);
                //}


                //スコア関連初期化
                score = 0.0d;
                hitTable = 0;
                hitCharPoint.Clear();
                totalPointCount = (nowData.PointCount * 5 + pointCount * 3);


                //パターンマッチング
                int height = dirTable.GetLength(1);
                int width = dirTable.GetLength(0);

                int matchAreaX = 0;
                int matchAreaY = 0;

                if (cm.Match == 1)
                {
                    matchAreaX = 2;
                    matchAreaY = 2;
                }
                else if (cm.Match == 2)
                {
                    matchAreaX = 3;
                    matchAreaY = 3;
                }
                else
                {
                    matchAreaX = 3;
                    matchAreaY = 4;
                }

                char[] dirs;
                bool isHit1;
                List<Point> lstPoint;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (dirTable[x, y] == ' ' || dirTable[x, y] == '0')
                        {
                            continue;
                        }

                        dirs = GetTargetDir(dirTable[x, y]);

                        //dirs = new char[]{(baseDirTable[x, y])};

                        isHit1 = false;

                        foreach (char dir in dirs)
                        {
                            lstPoint = nowData.GetDirPoint(dir);

                            foreach (Point p in lstPoint)
                            {
                                if (Math.Abs(p.X - x) <= matchAreaX && Math.Abs(p.Y - y) <= matchAreaY)
                                {
                                    if (!isHit1)
                                    {
                                        isHit1 = true;

                                        if (dir == dirTable[x, y])
                                        {
                                            hitTable += 2;
                                        }
                                        else
                                        {
                                            hitTable += 1;
                                        }
                                    }

                                    if (!hitCharPoint.Contains(p))
                                    {
                                        hitCharPoint.Add(p);
                                        hitTable++;

                                        goto LOOP_END;
                                    }
                                }
                            }
                        }

                    LOOP_END:

                        ;

                    }
                }

                score = Convert.ToDouble((hitTable + hitCharPoint.Count * 5)) / Convert.ToDouble(totalPointCount) * 100;

                if (maxScore < score)
                {
                    maxScore = score;
                    matchData = nowData;

                    if (maxScore >= this.cm.Score2)
                    {
                        return new object[] { matchData, maxScore };
                    }
                }
            }

            if (maxScore < this.cm.Score1)
            {
                return new object[] { this.charm[' '], 0.0d };
            }

            return new object[] { matchData, maxScore };
        }

        public object[] GetMatchData(int left,char addc)
        {
            CharData matchData = null;
            CharData nowData = null;
            char[,] baseTable = null;
            char[,] baseDirTable = null;
            char[,] dirTable = null;
            int pointCount = 0;

            int totalPointCount = 0;
            double score = 0.0d;
            HashSet<Point> hitCharPoint = new HashSet<Point>();
            int hitTable = 0;

            double maxScore = double.MinValue;

            baseTable = AAUtil.TrimTable(this.table, 0, left, this.table.GetLength(1), this.charm[this.charm.Count - 1].Width, 1);

            baseDirTable = new PatturnBuilder(baseTable).Patturn;

            for (int i = 0; i < this.charm.Count; i++)
            {
                nowData = this.charm[i];

                //AAUtil.DebugTable(nowData.Patturn);

                //テーブル初期化
                if (i == 0 || this.charm[i - 1].Width != nowData.Width)
                {
                    //baseTable = AAUtil.TrimTable(this.table, 0, left, this.table.GetLength(1), nowData.Width, 1);

                    dirTable = AAUtil.TrimTable(baseDirTable, 0, 0, nowData.Height, nowData.Width, 0);

                    pointCount = 0;

                    for (int y = 0; y < dirTable.GetLength(1); y++)
                    {
                        for (int x = 0; x < dirTable.GetLength(0); x++)
                        {
                            if (dirTable[x, y] != ' ')
                            {
                                pointCount++;
                            }
                        }
                    }


                    //if (i == 0)
                    //{
                    //    AAUtil.DebugTable(this.table);
                    //    AAUtil.DebugTable(baseTable);
                    //    AAUtil.DebugTable(baseDirTable);
                    //    AAUtil.DebugTable(nowData.Patturn);
                    //    AAUtil.DebugTable(nowData.NormalPatturn);
                    //}
                }

                if (i == 0 && pointCount <= 2)
                {
                    return new object[] { this.charm[' '], 0.0d };
                }

                if (nowData.Character == '｜' && addc == '.')
                {
                    continue;
                }

                if (nowData.IsHorizonLine && addc != '\0')
                {
                    continue;
                }

                //if (nowData.IsTarget == true)
                //{
                //    continue;
                //}


                //スコア関連初期化
                score = 0.0d;
                hitTable = 0;
                hitCharPoint.Clear();
                totalPointCount = (nowData.PointNormalCount * 5 + pointCount * 3);

                if (totalPointCount == 0)
                {
                    continue;
                }

                //パターンマッチング
                int height = dirTable.GetLength(1);
                int width = dirTable.GetLength(0);

                int matchAreaX = 0;
                int matchAreaY = 0;

                if (cm.Match == 1)
                {
                    matchAreaX = 1;
                    matchAreaY = 1;
                }
                else if (cm.Match == 2)
                {
                    matchAreaX = 2;
                    matchAreaY = 2;
                }
                else
                {
                    matchAreaX = 3;
                    matchAreaY = 3;
                }

                bool isHit1;
                List<Point> lstPoint;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {

                        if (dirTable[x, y] == ' ')
                        {
                            continue;
                        }



                        isHit1 = false;

                        lstPoint = nowData.GetDirPoint('N');

                        foreach (Point p in lstPoint)
                        {
                            if (Math.Abs(p.X - x) <= matchAreaX && Math.Abs(p.Y - y) <= matchAreaY)
                            {
                                if (!isHit1)
                                {
                                    isHit1 = true;

                                    hitTable += 2;
                                }

                                if (!hitCharPoint.Contains(p))
                                {
                                    hitCharPoint.Add(p);
                                    hitTable++;

                                    // Debug.WriteLine(p);

                                    goto LOOP_END;
                                }
                            }
                        }

                    LOOP_END:

                        ;

                    }
                }

                score = Convert.ToDouble((hitTable + hitCharPoint.Count * 5)) / Convert.ToDouble(totalPointCount) * 100;

                if (maxScore < score)
                {
                    maxScore = score;
                    matchData = nowData;

                    if (maxScore >= this.cm.Score4)
                    {
                        return new object[] { matchData, maxScore };
                    }
                }
            }

            if (maxScore < this.cm.Score3)
            {
                return new object[] { this.charm[' '], 0.0d };
            }

            if (matchData == null)
            {
                return new object[] { this.charm[' '], 0.0d };
            }

            return new object[] { matchData, maxScore };
        }

        private char[] GetTargetDir(char dir)
        {
            if (this.cm.Angle == 1)
            {
                return new char[] { dir };
            }

            switch (dir)
            {
                case '1':
                    return new char[] { '1', '2', '8' };

                case '2':
                    return new char[] { '2', '3', '1' };

                case '3':
                    return new char[] { '3', '4', '2' };

                case '4':
                    return new char[] { '4', '5', '3' };

                case '5':
                    return new char[] { '5', '6', '4' };

                case '6':
                    return new char[] { '6', '7', '5' };

                case '7':
                    return new char[] { '7', '8', '6' };

                case '8':
                    return new char[] { '8', '1', '7' };

            }

            throw new Exception("方向エラー");
        }

    }
}
