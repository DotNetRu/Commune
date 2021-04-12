using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Abstractions;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetRu.GitHubFilesystem
{
    internal class GitHubFile : IFile
    {
        public GitHubFile([NotNull] IFileSystem fileSystem)
        {
            FileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }

        public async Task AppendAllLinesAsync(string path, IEnumerable<string> contents,
            CancellationToken cancellationToken = new CancellationToken()) =>
            throw new NotImplementedException();

        public async Task AppendAllLinesAsync(string path, IEnumerable<string> contents, Encoding encoding,
            CancellationToken cancellationToken = new CancellationToken()) =>
            throw new NotImplementedException();

        public async Task AppendAllTextAsync(string path, string contents, CancellationToken cancellationToken = new CancellationToken()) => throw new NotImplementedException();

        public async Task AppendAllTextAsync(string path, string contents, Encoding encoding,
            CancellationToken cancellationToken = new CancellationToken()) =>
            throw new NotImplementedException();

        public async Task<byte[]> ReadAllBytesAsync(string path, CancellationToken cancellationToken = new CancellationToken()) => throw new NotImplementedException();

        public async Task<string[]> ReadAllLinesAsync(string path, CancellationToken cancellationToken = new CancellationToken()) => throw new NotImplementedException();

        public async Task<string[]> ReadAllLinesAsync(string path, Encoding encoding, CancellationToken cancellationToken = new CancellationToken()) => throw new NotImplementedException();

        public async Task<string> ReadAllTextAsync(string path, CancellationToken cancellationToken = new CancellationToken()) => throw new NotImplementedException();

        public async Task<string> ReadAllTextAsync(string path, Encoding encoding, CancellationToken cancellationToken = new CancellationToken()) => throw new NotImplementedException();

        public async Task WriteAllLinesAsync(string path, IEnumerable<string> contents,
            CancellationToken cancellationToken = new CancellationToken()) =>
            throw new NotImplementedException();

        public async Task WriteAllLinesAsync(string path, IEnumerable<string> contents, Encoding encoding,
            CancellationToken cancellationToken = new CancellationToken()) =>
            throw new NotImplementedException();

        public async Task WriteAllLinesAsync(string path, string[] contents, CancellationToken cancellationToken = new CancellationToken()) => throw new NotImplementedException();

        public async Task WriteAllLinesAsync(string path, string[] contents, Encoding encoding,
            CancellationToken cancellationToken = new CancellationToken()) =>
            throw new NotImplementedException();

        public async Task WriteAllTextAsync(string path, string contents, CancellationToken cancellationToken = new CancellationToken()) => throw new NotImplementedException();

        public async Task WriteAllTextAsync(string path, string contents, Encoding encoding,
            CancellationToken cancellationToken = new CancellationToken()) =>
            throw new NotImplementedException();

        public async Task WriteAllBytesAsync(string path, byte[] bytes, CancellationToken cancellationToken = new CancellationToken()) => throw new NotImplementedException();

        public void AppendAllLines(string path, IEnumerable<string> contents) => throw new NotImplementedException();

        public void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding) => throw new NotImplementedException();

        public void AppendAllText(string path, string contents) => throw new NotImplementedException();

        public void AppendAllText(string path, string contents, Encoding encoding) => throw new NotImplementedException();

        public StreamWriter AppendText(string path) => throw new NotImplementedException();

        public void Copy(string sourceFileName, string destFileName) => throw new NotImplementedException();

        public void Copy(string sourceFileName, string destFileName, bool overwrite) => throw new NotImplementedException();

        public Stream Create(string path) => throw new NotImplementedException();

        public Stream Create(string path, int bufferSize) => throw new NotImplementedException();

        public Stream Create(string path, int bufferSize, FileOptions options) => throw new NotImplementedException();

        public StreamWriter CreateText(string path) => throw new NotImplementedException();

        public void Decrypt(string path) => throw new NotImplementedException();

        public void Delete(string path) => throw new NotImplementedException();

        public void Encrypt(string path) => throw new NotImplementedException();

        public bool Exists(string path) => throw new NotImplementedException();

        public FileSecurity GetAccessControl(string path) => throw new NotImplementedException();

        public FileSecurity GetAccessControl(string path, AccessControlSections includeSections) => throw new NotImplementedException();

        public FileAttributes GetAttributes(string path) => throw new NotImplementedException();

        public DateTime GetCreationTime(string path) => throw new NotImplementedException();

        public DateTime GetCreationTimeUtc(string path) => throw new NotImplementedException();

        public DateTime GetLastAccessTime(string path) => throw new NotImplementedException();

        public DateTime GetLastAccessTimeUtc(string path) => throw new NotImplementedException();

        public DateTime GetLastWriteTime(string path) => throw new NotImplementedException();

        public DateTime GetLastWriteTimeUtc(string path) => throw new NotImplementedException();

        public void Move(string sourceFileName, string destFileName) => throw new NotImplementedException();

        public void Move(string sourceFileName, string destFileName, bool overwrite) => throw new NotImplementedException();

        public Stream Open(string path, FileMode mode) => throw new NotImplementedException();

        public Stream Open(string path, FileMode mode, FileAccess access) => throw new NotImplementedException();

        public Stream Open(string path, FileMode mode, FileAccess access, FileShare share) => throw new NotImplementedException();

        public Stream OpenRead(string path) => throw new NotImplementedException();

        public StreamReader OpenText(string path) => throw new NotImplementedException();

        public Stream OpenWrite(string path) => throw new NotImplementedException();

        public byte[] ReadAllBytes(string path) => throw new NotImplementedException();

        public string[] ReadAllLines(string path) => throw new NotImplementedException();

        public string[] ReadAllLines(string path, Encoding encoding) => throw new NotImplementedException();

        public string ReadAllText(string path) => throw new NotImplementedException();

        public string ReadAllText(string path, Encoding encoding) => throw new NotImplementedException();

        public IEnumerable<string> ReadLines(string path) => throw new NotImplementedException();

        public IEnumerable<string> ReadLines(string path, Encoding encoding) => throw new NotImplementedException();

        public void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName) => throw new NotImplementedException();

        public void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName,
            bool ignoreMetadataErrors) =>
            throw new NotImplementedException();

        public void SetAccessControl(string path, FileSecurity fileSecurity) => throw new NotImplementedException();

        public void SetAttributes(string path, FileAttributes fileAttributes) => throw new NotImplementedException();

        public void SetCreationTime(string path, DateTime creationTime) => throw new NotImplementedException();

        public void SetCreationTimeUtc(string path, DateTime creationTimeUtc) => throw new NotImplementedException();

        public void SetLastAccessTime(string path, DateTime lastAccessTime) => throw new NotImplementedException();

        public void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc) => throw new NotImplementedException();

        public void SetLastWriteTime(string path, DateTime lastWriteTime) => throw new NotImplementedException();

        public void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc) => throw new NotImplementedException();

        public void WriteAllBytes(string path, byte[] bytes) => throw new NotImplementedException();

        public void WriteAllLines(string path, IEnumerable<string> contents) => throw new NotImplementedException();

        public void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding) => throw new NotImplementedException();

        public void WriteAllLines(string path, string[] contents) => throw new NotImplementedException();

        public void WriteAllLines(string path, string[] contents, Encoding encoding) => throw new NotImplementedException();

        public void WriteAllText(string path, string contents) => throw new NotImplementedException();

        public void WriteAllText(string path, string contents, Encoding encoding) => throw new NotImplementedException();

        public IFileSystem FileSystem { get; }
    }
}
