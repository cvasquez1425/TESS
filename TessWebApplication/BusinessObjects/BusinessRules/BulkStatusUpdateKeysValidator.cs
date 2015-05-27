using System.Collections.Generic;
using Greenspoon.Tess.DataObjects.Linq;

namespace Greenspoon.Tess.BusinessObjects.BusinessRules
{
    public class BulkStatusUpdateKeysValidator
    {
        readonly Dictionary<int, IBatchKeyValidator> _validatorList;
        readonly string _keys;
        readonly int _identifier;
        public BulkStatusUpdateKeysValidator(string keys, int iden)
        {
            _keys = keys;
            _identifier = iden;
            _validatorList = new Dictionary<int, IBatchKeyValidator>
                                {
                                    {1, new ValidateBatchEscrowKeys()},
                                    {2, new ValidateContractKeys()},
                                    {3, new ValidateCancelKeys()},
                                    {4, new ValidateDevKKeys()},
                                    {5, new ValidateClientBatchKeys()}                  //  Adding Client Batch as a new Identifier August 2014
                                };
        }

        public IEnumerable<string> GetInvalidKeys()
        {
            return !_validatorList.ContainsKey(_identifier) ? null : _validatorList[_identifier].GetInvalidIds(_keys);
        }
    }

    public interface IBatchKeyValidator
    {
        IEnumerable<string> GetInvalidIds(string keys);
    }

    public class ValidateBatchEscrowKeys : IBatchKeyValidator
    {
        public IEnumerable<string> GetInvalidIds(string keys)
        {
            return batch_escrow.GetInvalidIds(keys);
        }
    }

    public class ValidateContractKeys : IBatchKeyValidator
    {
        public IEnumerable<string> GetInvalidIds(string keys)
        {
            return contract.GetInvalidIds(keys);
        }
    }

    public class ValidateDevKKeys : IBatchKeyValidator
    {
        public IEnumerable<string> GetInvalidIds(string keys)
        {
            return contract.GetInvalidDevKIds(keys);
        }
    }

    public class ValidateCancelKeys : IBatchKeyValidator
    {
        public IEnumerable<string> GetInvalidIds(string keys)
        {
            return batch_cancel.GetInvalidIds(keys);
        }
    }

    //  Adding Client Batch as a new Identifier August 2014
    public class ValidateClientBatchKeys : IBatchKeyValidator
    {
        public IEnumerable<string> GetInvalidIds(string keys)
        {
            return contract.GetInvalidClientBatchIds(keys);
        }
    }
}