﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DataModels
{
public class TrBpkb
                {
                    public string AgreementNumber { get; set; }
                    public string BpkbNo { get; set; }
                    public string BranchId { get; set; }
                    public DateTime BpkbDate { get; set; }
                    public string FakturNo { get; set; }
                    public DateTime FakturDate { get; set; }
                    public string LocationId { get; set; }
                    public string PoliceNo { get; set; }
                    public DateTime BpkbDateIn { get; set; }
                    public string CreatedBy { get; set; }
                    public DateTime CreatedOn { get; set; }
                    public string LastUpdatedBy { get; set; }
                    public DateTime LastUpdatedOn { get; set; }

                    public MsStorageLocation Location { get; set; }
                }

}
