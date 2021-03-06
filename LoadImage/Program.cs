﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace dotNetStiEditor
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			// локализация
			cultureInfo = System.Configuration.ConfigurationManager.AppSettings.Get("cultureInfo");
			if (cultureInfo == null || cultureInfo.Length == 0)
			{
				SelectCulutreForm cultureForm = new SelectCulutreForm();
				if (cultureForm.ShowDialog() == DialogResult.OK)
					cultureInfo = cultureForm.CultureInfo;
				else
					cultureInfo = "ru";
				try
				{
					System.Threading.Thread.CurrentThread.CurrentUICulture =
						new System.Globalization.CultureInfo(cultureInfo);
				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex.Message);
					cultureInfo = "ru";
				}
			}
			else
			{
				try
				{
					System.Threading.Thread.CurrentThread.CurrentUICulture =
						new System.Globalization.CultureInfo(cultureInfo);
				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex.Message);
				}
				cultureInfo = "";
			}

			Application.EnableVisualStyles();
			//	Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new EditorMainForm(cultureInfo));
		}

		private static string cultureInfo;
	}
}