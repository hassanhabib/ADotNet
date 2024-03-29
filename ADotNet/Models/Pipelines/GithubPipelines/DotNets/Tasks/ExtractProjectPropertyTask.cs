﻿// ---------------------------------------------------------------------------
// Copyright (c) Hassan Habib & Shri Humrudha Jagathisun All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------------------

using YamlDotNet.Serialization;

namespace ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks
{
    /// <summary>
    /// A task to build .NET project.
    /// </summary>
    public sealed class ExtractProjectPropertyTask : GithubTask
    {
        public ExtractProjectPropertyTask(
            string name,
            string id,
            string projectRelativePath,
            string propertyName,
            string stepVariableName,
            string runsOn)
        {
            Name = name;
            Id = id;

            if (runsOn.ToLower().StartsWith("windows"))
            {
                Shell = ShellEnvironments.PowerShellCore;

                Run =
                    "# Running on Windows \n"
                    + "$" + stepVariableName + "=((Select-Xml -Path '" + projectRelativePath + "' -XPath '//" + propertyName + "').Node.InnerXML)\n"
                    + "echo \"$" + stepVariableName + "\"\n"
                    + "echo \"" + stepVariableName + "<<EOF\" >> $GITHUB_OUTPUT \n"
                    + "echo \"$" + stepVariableName + "\" >> $GITHUB_OUTPUT \n"
                    + "echo \"EOF\" >> $GITHUB_OUTPUT \n";
            }
            else
            {
                Shell = ShellEnvironments.Bash;

                Run =
                    "# Running on Linux/Unix \n"
                    + "sudo apt-get install xmlstarlet\n"
                    + stepVariableName + "=$(xmlstarlet sel -t -v \"//" + propertyName + "\" -n " + projectRelativePath + ")\n"
                    + "echo \"$" + stepVariableName + "\"\n"
                    + "echo \"" + stepVariableName + "<<EOF\" >> $GITHUB_OUTPUT \n"
                    + "echo \"$" + stepVariableName + "\" >> $GITHUB_OUTPUT \n"
                    + "echo \"EOF\" >> $GITHUB_OUTPUT \n";
            }
        }

        /// <summary>
        /// Gets the name of the task.
        /// </summary>
        [YamlMember(Order = 0, DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new string Name { get; private set; }

        /// <summary>
        /// Gets the unique identifier of the task.
        /// </summary>
        [YamlMember(Order = 1, DefaultValuesHandling = DefaultValuesHandling.OmitDefaults)]
        public new string Id { get; private set; }

        /// <summary>
        /// Gets the command to execute for the task.
        /// </summary>
        [YamlMember(Order = 7, Alias = "run")]
        public new string Run { get; private set; }

        /// <summary>
        /// Gets the shell on which the task is executed.
        /// </summary>
        [YamlMember(Order = 9, Alias = "shell")]
        public new string Shell { get; private set; }
    }
}
