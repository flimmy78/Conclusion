﻿namespace Library.Common
{
    partial class levelmeter
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.delevelmeter = new System.Windows.Forms.ProgressBar();
            // 
            // progressBar1
            // 
            this.delevelmeter.Location = new System.Drawing.Point(0, 0);
            this.delevelmeter.Name = "levelmeter";
            this.delevelmeter.Size = new System.Drawing.Size(100, 23);
            this.delevelmeter.TabIndex = 0;
            this.delevelmeter.Click += new System.EventHandler(this.levelmeter_Click);
            this.delevelmeter.Minimum = 0;
            this.delevelmeter.Value = 50;
        }

        #endregion

        private System.Windows.Forms.ProgressBar delevelmeter;
    }
}
