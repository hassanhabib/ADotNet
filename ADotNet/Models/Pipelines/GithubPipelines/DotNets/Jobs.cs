﻿// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using System;
using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets
{
    [Obsolete("No longer in use.")]
    public class Jobs
    {
        [YamlMember(Order = 0)]
        public BuildJob Build { get; set; }

        [YamlMember(Order = 1, Alias = "add_tag", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public TagJob AddTag { get; set; }

        [YamlMember(Order = 2, Alias = "publish", DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public PublishJob Publish { get; set; }
    }
}
