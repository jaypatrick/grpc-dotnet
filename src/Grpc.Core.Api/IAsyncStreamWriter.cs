#region Copyright notice and license

// Copyright 2015 gRPC authors.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Grpc.Core;

/// <summary>
/// A writable stream of messages.
/// </summary>
/// <typeparam name="T">The message type.</typeparam>
public interface IAsyncStreamWriter<in T>
{
    /// <summary>
    /// Writes a message asynchronously. Only one write can be pending at a time.
    /// </summary>
    /// <param name="message">The message to be written. Cannot be null.</param>
    Task WriteAsync(T message);

#if NETSTANDARD2_1_OR_GREATER
    /// <summary>
    /// Writes a message asynchronously. Only one write can be pending at a time.
    /// </summary>
    /// <param name="message">The message to be written. Cannot be null.</param>
    /// <param name="cancellationToken">Cancellation token that can be used to cancel the operation.</param>
    Task WriteAsync(T message, CancellationToken cancellationToken)
    {
        if (cancellationToken.CanBeCanceled)
        {
            // Note to implementors:
            // Add a netstandard2.1 or greater target to your library and override
            // WriteAsync(T, CancellationToken) on stream writer to use the cancellation token.
            throw new NotSupportedException("Cancellation of stream writes is not supported by this gRPC implementation.");
        }

        return WriteAsync(message);
    }
#endif

    /// <summary>
    /// Write options that will be used for the next write.
    /// If null, default options will be used.
    /// Once set, this property maintains its value across subsequent
    /// writes.
    /// </summary>
    WriteOptions? WriteOptions { get; set; }
}
