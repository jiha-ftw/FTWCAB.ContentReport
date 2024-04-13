import './table-header.css'
import Icon from "@/components/icon/Icon";

const TableHeader = ({ children, reload }) => {
    return <header className="axiom-typography--header3 table-header">
        { children }

        <span title="Reload" onClick={reload}>
            <Icon icon="loop2" size={20} />
        </span>
    </header>;
};

export default TableHeader;