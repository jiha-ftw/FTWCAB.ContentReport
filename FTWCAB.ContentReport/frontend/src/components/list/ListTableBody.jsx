const ListTableBody = ({ items, pageSize, setSelectedContentInstance }) => <>
    {!items.length &&
        <tr className="mdc-data-table__row">
            <td className="mdc-data-table__cell" colSpan={5} style={{ textAlign: 'center' }}>No instances of this type of content</td>
        </tr>
    }
    {items.map((i, index) =>
        <tr className="mdc-data-table__row" key={index}>
            <td className="mdc-data-table__cell">{i.id}</td>
            <td className="mdc-data-table__cell"><a href={i.editLink} target="_blank" rel="noreferrer">Edit</a></td>
            <td className="mdc-data-table__cell">{i.name}</td>
            <td className="mdc-data-table__cell">
                <button type="button" onClick={() => setSelectedContentInstance(i)} className="oui-button oui-button--outline oui-button--default">
                    Show
                </button>
            </td>
            <td className="mdc-data-table__cell">
                {i.parentEditLink && i.parentName && <a href={i.parentEditLink} target="_blank" rel="noreferrer">
                    {i.parentName} ({i.parentContentTypeName})
                </a>}
            </td>
        </tr>
    )}
    {
        !!(items.length && pageSize - items.length) && [...Array(pageSize - items.length)].map((_emptyRow, index) =>
            <tr className="mdc-data-table__row" key={index + items.length}>
                <td colSpan={5} className="mdc-data-table__cell"></td>
            </tr>)
    }
</>;

export default ListTableBody;