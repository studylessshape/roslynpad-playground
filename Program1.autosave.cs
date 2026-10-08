var ty = typeof(OperationWorkStepExcel);
ty.GetProperties()
    .Select(p => p.Name)
    .Dump();
    
    public class OperationWorkStepExcel
    {
        #region 工序基础数据
        /// <summary>
        /// 工序名
        /// </summary>
        public string Name { get; set; } = string.Empty;
        public string? NextOperationName { get; set; }

        public bool VerfiyAGV { get; set; }

        public int? DesignCycleTime { get; set; }

        /// <summary>
        /// 工作的工站
        /// </summary>
        public string? Stations { get; set; }
        #endregion

        #region 工步基础数据
        /// <summary>
        /// 工步名
        /// </summary>
        public string StepName { get; set; } = string.Empty;
        /// <summary>
        /// 工步详细名
        /// </summary>
        public string StepLongName { get; set; } = string.Empty;
        /// <summary>
        /// 工步类型
        /// </summary>
        public string StepKind { get; set; } = string.Empty;
        /// <summary>
        /// 上传代码
        /// </summary>
        public string? UploadCode { get; set; }
        /// <summary>
        /// 上传代码后缀
        /// </summary>
        
        public string? UploadCodeSuffix { get; set; }
        /// <summary>
        /// 工步关键字
        /// </summary>
        
        public string? ProcKey { get; set; }
        #endregion

        #region 输入任务通用
        /// <summary>
        /// 是否本地校验
        /// </summary>
        
        public bool? IsLocalCheck { get; set; }
        /// <summary>
        /// 唯一性校验
        /// </summary>
        
        public bool? IsCheckUnique { get; set; }
        #endregion

        #region 扫码任务通用

        
        public string? GunRule { get; set; }
        
        public string? CodeRule { get; set; }
        #endregion

        #region 扫描出货码
        
        public bool? ScanDeliveryCode_BindAGV { get; set; }
        #endregion

        #region 精追批追
        
        public string? BomItemCode { get; set; }
        
        public decimal? Consumption { get; set; }
        
        public string? BomItemPosition { get; set; }
        #endregion

        #region 精追
        /// <summary>
        /// 内部码规则
        /// </summary>
        
        public string? PreciseTracing_InternalCodeRule { get; set; }
        /// <summary>
        /// 外部码规则
        /// </summary>
        
        public string? PreciseTracing_ExternalCodeRule { get; set; }
        #endregion

        #region 扫码比对
        
        public int? ScanEqualCode_CompareCount { get; set; }

        
        public string? ScanEqualCode_CompareMode { get; set; }
        #endregion

        #region 数值输入
        
        public decimal? UserInputDecimal_Min { get; set; }
        
        public decimal? UserInputDecimal_Max { get; set; }
        #endregion

        #region 放行
        
        public bool? LetGo_IsDataCollectEnabled { get; set; }
        #endregion

        #region 超时检测
        
        public string? TimeoutCheck_TargetProcKey { get; set; }
        
        public string? TimeoutCheck_TimeoutRange { get; set; }
        #endregion

        #region 耗时检测
        public string? TimeCostCheck_TargetProcKey { get; set; }
        public string? TimeCostCheck_TimeoutRange { get; set; }
        #endregion

        #region 称重
        public string? Weighting_DeviceNo { get; set; }
        public decimal? Weighting_K { get; set; }
        public decimal? Weighting_B { get; set; }
        public decimal? Weighting_Max { get; set; }
        public decimal? Weighting_Min { get; set; }
        #endregion

        #region 充气
        public decimal? Leak_LeakTime { get; set; }
        public decimal? Leak_LeakPressure { get; set; }
        public decimal? Leak_LeakKeepTime { get; set; }
        public decimal? Leak_LeakKeepPressure { get; set; }
        public string? Leak_LeakTimeUploadCode { get; set; }
        public string? Leak_LeakPressureUploadCode { get; set; }
        public string? Leak_LeakKeepTimeUploadCode { get; set; }
        public string? Leak_LeakKeepPressureUploadCode { get; set; }
        #endregion

        #region 拧紧基础数据
        public int? ScrewBolt_ProgNo { get; set; }
        public bool? ScrewBolt_AutoBackwardEnable { get; set; }
        public int? ScrewBolt_BackwardProgNo { get; set; }
        public decimal? ScrewBolt_TorqueMin { get; set; }
        public decimal? ScrewBolt_TorqueMax { get; set; }
        public decimal? ScrewBolt_AngleMin { get; set; }
        public decimal? ScrewBolt_AngleMax { get; set; }
        #endregion

        #region 拧紧枪配置
        public string? ScrewBolt_GunCodes { get; set; }
        public int? ScrewBolt_StartOrder { get; set; }
        public int? ScrewBolt_EndOrder { get; set; }
        public int? ScrewBolt_SockNo { get; set; }
        #endregion

        #region 补拧
        /// <summary>
        /// 补拧数据来源（来自选定工序 / 来自接口）
        /// </summary>
        public string? ScrewFix_AutoDataSource { get; set; }
        public string? ScrewFix_TargetOperation { get; set; }
        #endregion

        #region 带图拧紧数据
        public string? ScrewBoltWithMap_ImagePrefix { get; set; }
        /// <summary>
        /// 用于占位，实际数据保存在 <see cref="ScrewBoltWithMap_Image"/> 中
        /// </summary>
        public object? ScrewBoltWithMap_ImagePlaced { get; set; } = null;
        public string? ScrewBoltWithMap_Points { get; set; }
        public string? ScrewBoltWithMap_GunTexts { get; set; }
        public byte[]? ScrewBoltWithMap_Image { get; set; }
        #endregion

        #region 额外数据
        public string? OpExtra { get; set; }
        public string? OpCode { get; set; }
        public string? NextOpCode { get; set; }
        public string? StepCode { get; set; }
        #endregion
    }
