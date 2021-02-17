using System.Collections.Generic;
using System.Threading.Tasks;

namespace Game.Services
{
    /// <summary>
    /// Interface for data intreactions
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataStore<T>
    {
        /// <summary>
        /// Create Async method for data store
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(T Data);

        /// <summary>
        /// Read Async method for datastore
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> ReadAsync(string id);

        /// <summary>
        /// Update Async method for datastore
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(T Data);

        /// <summary>
        /// DeleteAsync method for datastore
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(string id);

        /// <summary>
        /// Index Async method for datastore
        /// </summary>
        /// <returns></returns>
        Task<List<T>> IndexAsync();

        /// <summary>
        /// Wipes the data list asynchronously 
        /// </summary>
        /// <returns></returns>
        Task<bool> WipeDataListAsync();

        /// <summary>
        /// Indicates if datastore needs initialization, asynchronously
        /// </summary>
        /// <returns></returns>
        Task<bool> GetNeedsInitializationAsync();
    }
}