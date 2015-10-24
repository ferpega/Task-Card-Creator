﻿// This source is subject to Microsoft Public License (Ms-PL).
// Please see http://taskcardcreator.codeplex.com for details.
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing.Printing;
using System.Windows.Documents;
using ReportingFramework;
using System.Windows;
using ReportInterface;

namespace Generic
{
  /// <summary>
  /// Interaction logic for Template.xaml
  /// </summary>
  [Export(typeof(IReport))]
  public partial class Template : ReportFromTemplate, IReport
  {
    public Template()
    {
      InitializeComponent();
    }

    public bool IsSupported(IEnumerable<string> wiTypeCollection)
    {
      /*
      if (wiTypeCollection == null)
      {
        return false;
      }
      if (wiTypeCollection.Count == 0)
      {
        return false;
      }
       */ 
      // Support all TFS templates
      return true;
    }

    public string Description
    {
      get { return "Generic Report"; }
    }

    public Size PaperSize { get { return new Size(8.27, 11.69); } }

    public Margins Margins
    {
      get
      {
        return new Margins(0, 0, 0, 0);
      }
    }

    public bool TeamCustomized { get { return false; } }

    public FixedDocument Create(IEnumerable<ReportItem> data)
    {
      var rows = new List<object>();
      foreach (var workItem in data)
      {
        rows.Add(new UnknownCardRow(workItem));
      }

      return GenerateReport(
          page => new PageInfo(page, DateTime.Now),
          PaperSize,
          Margins,
          rows
          );
    }

    /// <summary>
    /// This class becomes the data context for every page. It gives the page 
    /// access to the page number.
    /// </summary>
    private class PageInfo
    {
      public PageInfo(int pageNumber, DateTime reportDate)
      {
          PageNumber = pageNumber;
          ReportDate = reportDate;
      }

      public bool IsFirstPage { get { return PageNumber == 1; } }
      public int PageNumber { get; set; }
      public DateTime ReportDate { get; set; }
    }
  }
}