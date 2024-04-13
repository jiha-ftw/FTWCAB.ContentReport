import Pager from '@/components/pager/pager';
import TableHeader from '../table-header/TableHeader';

const Table = ({ reload, header, tableHeader, tableBody, currentPage, setCurrentPage, pages }) => {

    return <>
        <TableHeader reload={reload}>
            {header}
        </TableHeader>
        <div className="mdc-data-table">
            <table className="mdc-data-table__table">
                <thead>
                    {tableHeader}
                </thead>
                <tbody>
                    {tableBody}
                </tbody>
            </table>
        </div>
        <Pager {...{ currentPage, setCurrentPage, pages }} />
    </>;
};

export default Table;