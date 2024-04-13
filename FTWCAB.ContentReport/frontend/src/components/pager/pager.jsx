import './pager.css'
import Icon from "@/components/icon/Icon";

const Pager = ({ currentPage, pages, setCurrentPage }) => {

    if (!pages) return <></>;

    return <div className="pager">

        <span title="First" className={`pager__arrow${!currentPage ? " pager__arrow--disabled" : ""}`}>
            <Icon icon="first" color={currentPage ? "black" : "gray"} size={20} onClick={() => setCurrentPage(0)} />
        </span>

        <span title="Previous" className={`pager__arrow${!currentPage ? " pager__arrow--disabled" : ""}`}>
            <Icon icon="arrow-left2" color={currentPage ? "black" : "gray"} size={20} onClick={() => setCurrentPage(c => Math.max(c - 1, 0))} />
        </span>

        <span>{currentPage + 1} of {pages}</span>

        <span title="Next" className={`pager__arrow${currentPage === pages - 1 ? " pager__arrow--disabled" : ""}`}>
            <Icon icon="arrow-right2" color={currentPage < pages - 1 ? "black" : "gray"} size={20} onClick={() => setCurrentPage(c => Math.min(c + 1, pages - 1))} />
        </span>

        <span title="Last" className={`pager__arrow${currentPage === pages - 1 ? " pager__arrow--disabled" : ""}`}>
            <Icon icon="last" color={currentPage < pages - 1 ? "black" : "gray"} size={20} onClick={() => setCurrentPage(c => pages - 1)} />
        </span>

    </div>;
};

export default Pager;