// 
// Copyright (c) 2004-2021 Jaroslaw Kowalski <jaak@jkowalski.net>, Kim Christensen, Julian Verdurmen
// 
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without 
// modification, are permitted provided that the following conditions 
// are met:
// 
// * Redistributions of source code must retain the above copyright notice, 
//   this list of conditions and the following disclaimer. 
// 
// * Redistributions in binary form must reproduce the above copyright notice,
//   this list of conditions and the following disclaimer in the documentation
//   and/or other materials provided with the distribution. 
// 
// * Neither the name of Jaroslaw Kowalski nor the names of its 
//   contributors may be used to endorse or promote products derived from this
//   software without specific prior written permission. 
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF 
// THE POSSIBILITY OF SUCH DAMAGE.
// 

namespace NLog.Targets
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using NLog.Config;
    using NLog.LayoutRenderers;
    using NLog.Layouts;

    /// <summary>
    /// Sends log messages to the remote instance of NLog Viewer. 
    /// </summary>
    /// <remarks>
    /// <a href="https://github.com/nlog/nlog/wiki/NLogViewer-target">See NLog Wiki</a>
    /// </remarks>
    /// <seealso href="https://github.com/nlog/nlog/wiki/NLogViewer-target">Documentation on NLog Wiki</seealso>
    /// <example>
    /// <p>
    /// To set up the target in the <a href="https://github.com/NLog/NLog/wiki/Configuration-file">configuration file</a>, 
    /// use the following syntax:
    /// </p>
    /// <code lang="XML" source="examples/targets/Configuration File/NLogViewer/NLog.config" />
    /// <p>
    /// To set up the log target programmatically use code like this:
    /// </p>
    /// <code lang="C#" source="examples/targets/Configuration API/NLogViewer/Simple/Example.cs" />
    /// </example>
    [Target("NLogViewer")]
    public class NLogViewerTarget : NetworkTarget, IIncludeContext
    {
        private readonly Log4JXmlEventLayout _log4JLayout = new Log4JXmlEventLayout();

        /// <summary>
        /// Initializes a new instance of the <see cref="NLogViewerTarget" /> class.
        /// </summary>
        /// <remarks>
        /// The default value of the layout is: <code>${longdate}|${level:uppercase=true}|${logger}|${message:withexception=true}</code>
        /// </remarks>
        public NLogViewerTarget()
        {
            IncludeNLogData = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NLogViewerTarget" /> class.
        /// </summary>
        /// <remarks>
        /// The default value of the layout is: <code>${longdate}|${level:uppercase=true}|${logger}|${message:withexception=true}</code>
        /// </remarks>
        /// <param name="name">Name of the target.</param>
        public NLogViewerTarget(string name) : this()
        {
            Name = name;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to include NLog-specific extensions to log4j schema.
        /// </summary>
        /// <docgen category='Layout Options' order='10' />
        public bool IncludeNLogData
        {
            get => Renderer.IncludeNLogData;
            set => Renderer.IncludeNLogData = value;
        }

        /// <summary>
        /// Gets or sets the AppInfo field. By default it's the friendly name of the current AppDomain.
        /// </summary>
        /// <docgen category='Layout Options' order='10' />
        public Layout AppInfo
        {
            get => Renderer.AppInfo;
            set => Renderer.AppInfo = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to include call site (class and method name) in the information sent over the network.
        /// </summary>
        /// <docgen category='Layout Options' order='10' />
        public bool IncludeCallSite
        {
            get => Renderer.IncludeCallSite;
            set => Renderer.IncludeCallSite = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to include source info (file name and line number) in the information sent over the network.
        /// </summary>
        /// <docgen category='Layout Options' order='10' />
        public bool IncludeSourceInfo
        {
            get => Renderer.IncludeSourceInfo;
            set => Renderer.IncludeSourceInfo = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to include <see cref="MappedDiagnosticsContext"/> dictionary contents.
        /// </summary>
        /// <docgen category='Layout Options' order='10' />
        [Obsolete("Replaced by IncludeScopeProperties. Marked obsolete on NLog 5.0")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool IncludeMdc
        {
            get => Renderer.IncludeMdc;
            set => Renderer.IncludeMdc = value;
        }

        /// <summary>
        /// Gets or sets whether to include log4j:NDC in output from <see cref="ScopeContext"/> nested context.
        /// </summary>
        /// <docgen category='Layout Options' order='10' />
        public bool IncludeNdc
        {
            get => Renderer.IncludeNdc;
            set => Renderer.IncludeNdc = value;
        }

        /// <inheritdoc/>
        /// <docgen category='Layout Options' order='10' />
        public bool IncludeEventProperties { get => Renderer.IncludeEventProperties; set => Renderer.IncludeEventProperties = value; }

        /// <inheritdoc/>
        /// <docgen category='Layout Options' order='10' />
        public bool IncludeScopeProperties { get => Renderer.IncludeScopeProperties; set => Renderer.IncludeScopeProperties = value; }

        /// <summary>
        /// Gets or sets whether to include log4j:NDC in output from <see cref="ScopeContext"/> nested context.
        /// </summary>
        /// <docgen category='Layout Options' order='10' />
        public bool IncludeScopeNested { get => Renderer.IncludeScopeNested; set => Renderer.IncludeScopeNested = value; }

        /// <summary>
        /// Gets or sets the separator for <see cref="ScopeContext"/> operation-states-stack.
        /// </summary>
        /// <docgen category='Layout Options' order='10' />
        public string ScopeNestedSeparator { get => Renderer.ScopeNestedSeparator; set => Renderer.ScopeNestedSeparator = value; }

        /// <summary>
        /// Gets or sets the option to include all properties from the log events
        /// </summary>
        /// <docgen category='Layout Options' order='10' />
        [Obsolete("Replaced by IncludeEventProperties. Marked obsolete on NLog 5.0")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool IncludeAllProperties { get => IncludeEventProperties; set => IncludeEventProperties = value; }

        /// <summary>
        /// Gets or sets a value indicating whether to include <see cref="MappedDiagnosticsLogicalContext"/> dictionary contents.
        /// </summary>
        /// <docgen category='Layout Options' order='10' />
        [Obsolete("Replaced by IncludeScopeProperties. Marked obsolete on NLog 5.0")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool IncludeMdlc { get => Renderer.IncludeMdlc; set => Renderer.IncludeMdlc = value; }

        /// <summary>
        /// Gets or sets a value indicating whether to include contents of the <see cref="NestedDiagnosticsLogicalContext"/> stack.
        /// </summary>
        /// <docgen category='Layout Options' order='10' />
        [Obsolete("Replaced by IncludeNdc. Marked obsolete on NLog 5.0")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool IncludeNdlc { get => Renderer.IncludeNdlc; set => Renderer.IncludeNdlc = value; }

        /// <summary>
        /// Gets or sets the stack separator for log4j:NDC in output from <see cref="ScopeContext"/> nested context.
        /// </summary>
        /// <docgen category='Layout Options' order='10' />
        [Obsolete("Replaced by NdcItemSeparator. Marked obsolete on NLog 5.0")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string NdlcItemSeparator { get => Renderer.NdlcItemSeparator; set => Renderer.NdlcItemSeparator = value; }

        /// <summary>
        /// Gets or sets the stack separator for log4j:NDC in output from <see cref="ScopeContext"/> nested context.
        /// </summary>
        /// <docgen category='Layout Options' order='10' />
        public string NdcItemSeparator
        {
            get => Renderer.NdcItemSeparator;
            set => Renderer.NdcItemSeparator = value;
        }

        /// <summary>
        /// Gets or sets the renderer for log4j:event logger-xml-attribute (Default ${logger})
        /// </summary>
        /// <docgen category='Layout Options' order='10' />
        public Layout LoggerName
        {
            get => Renderer.LoggerName;
            set => Renderer.LoggerName = value;
        }

        /// <summary>
        /// Gets the collection of parameters. Each parameter contains a mapping
        /// between NLog layout and a named parameter.
        /// </summary>
        /// <docgen category='Layout Options' order='10' />
        [ArrayParameter(typeof(NLogViewerParameterInfo), "parameter")]
        public IList<NLogViewerParameterInfo> Parameters => _log4JLayout.Parameters;

        /// <summary>
        /// Gets the layout renderer which produces Log4j-compatible XML events.
        /// </summary>
        [NLogConfigurationIgnoreProperty]
        public Log4JXmlEventLayoutRenderer Renderer => _log4JLayout.Renderer;

        /// <summary>
        /// Gets or sets the instance of <see cref="Log4JXmlEventLayout"/> that is used to format log messages.
        /// </summary>
        /// <docgen category='Layout Options' order='1' />
        public override Layout Layout
        {
            get
            {
                return _log4JLayout;
            }
            set
            {
                // Fixed Log4JXmlEventLayout
            }
        }
    }
}
