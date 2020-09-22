using MiroCommunity_API.Models.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiroCommunity_API
{
    /// <summary>
    ///
    /// </summary>
    public class Result
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; } = 200;

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 同步不需要传入参数且无返回数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Result ReturnResult<T>(T t, Action<T> func)
        {
            Result result = new Result();
            try
            {
                func(t);
            }
            catch (BusinessException ex)
            {
                result.Code = ex.Code;
                result.Message = ex.Message;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        ///异步方法需要传入参数且无返回数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static async Task<Result> ReturnResult<T>(T t, Func<T, Task> func)
        {
            Result result = new Result();
            try
            {
                await func(t);
            }
            catch (BusinessException ex)
            {
                result.Code = ex.Code;
                result.Message = ex.Message;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }
            return result;
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultModels<T> : Result
    {
        /// <summary>
        /// 需要返回的数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 同步需要传入参数
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="k"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static ResultModels<T> ReturnResult<K>(K k, Func<K, T> func)
        {
            ResultModels<T> result = new ResultModels<T>();
            try
            {
                result.Data = func(k);
            }
            catch (BusinessException ex)
            {
                result.Code = ex.Code;
                result.Message = ex.Message;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 同步不需要传入参数
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public static ResultModels<T> ReturnResult(Func<T> func)
        {
            ResultModels<T> result = new ResultModels<T>();
            try
            {
                result.Data = func();
            }
            catch (BusinessException ex)
            {
                result.Code = ex.Code;
                result.Message = ex.Message;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        ///异步需要传入数据有返回数据
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="k"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static async Task<ResultModels<T>> ReturnResult<K>(K k, Func<K, Task<T>> func)
        {
            ResultModels<T> result = new ResultModels<T>();
            try
            {
                result.Data = await func(k);
            }
            catch (BusinessException ex)
            {
                result.Code = ex.Code;
                result.Message = ex.Message;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 异步不需要传入数据有返回数据
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public static async Task<ResultModels<T>> ReturnResult(Func<Task<T>> func)
        {
            ResultModels<T> result = new ResultModels<T>();
            try
            {
                result.Data = await func();
            }
            catch (BusinessException ex)
            {
                result.Code = ex.Code;
                result.Message = ex.Message;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}