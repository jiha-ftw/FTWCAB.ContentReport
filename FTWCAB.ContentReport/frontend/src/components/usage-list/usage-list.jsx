import './usage-list.css';
import { useEffect, useState } from 'react';
import { fetchContentTypeUsages } from '@/store/contentUsage';
import Table from '@/components/table/table';
import UsageListBody from './usage-list-body';
import Icon from '@/components/icon/Icon';
import { setSelectedContentInstance } from '@/store/contentUsage';
import { useAppSelector, useAppDispatch } from '@/hooks'

const UsageList = () => {
    const dispatch = useAppDispatch();
    const [currentPage, setCurrentPage] = useState(0);
    const { usages, loaded, pages, totalCount, pageSize, selectedContentInstance } = useAppSelector(state => state.contentUsage);

    const loadUsages = () => dispatch(fetchContentTypeUsages({ contentInstanceId: selectedContentInstance?.id, page: currentPage }));

    useEffect(() => {
        selectedContentInstance?.id && loadUsages();
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [selectedContentInstance, currentPage])

    if (selectedContentInstance === null) return <></>;

    return <div
        className={`usage-list-overlay${true ? "" : " usage-list-overlay--hidden"}`}
        onClick={() => dispatch(setSelectedContentInstance(null))}>

        <div className="usage-list-overlay__content" onClick={(e) => e.stopPropagation()}>

            {(!loaded && <div>Loading...</div>) || <>

                <span
                    title="Close"
                    className="usage-list-overlay__close-btn"
                    onClick={() => dispatch(setSelectedContentInstance(null))}>
                    <Icon icon="cross" size={20} />
                </span>

                <Table {...{ instances: usages, pageSize, currentPage, setCurrentPage, pages, reload: () => { setCurrentPage(0); loadUsages(); } }}
                    header={<>{totalCount} usage(s) of: {selectedContentInstance?.name} (Content Id: {selectedContentInstance?.id})</>}
                    tableHeader={<tr>
                        <th className="mdc-data-table__header-cell">Id</th>
                        <th className="mdc-data-table__header-cell">Edit link</th>
                        <th className="mdc-data-table__header-cell" style={{ width: '100%' }}>Name</th>
                    </tr>}
                    tableBody={<UsageListBody {...{ usages, pageSize }} />}
                />
            </>}
        </div>
    </div>
};

export default UsageList;