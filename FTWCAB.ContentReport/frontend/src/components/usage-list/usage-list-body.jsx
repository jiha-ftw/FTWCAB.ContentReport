const UsageListBody = ({ usages, pageSize }) => {
    return <>
        {!usages.length &&
            <tr className="mdc-data-table__row">
                <td className="mdc-data-table__cell" colSpan={5} style={{ textAlign: 'center' }}>No usage for this type of content</td>
            </tr>
        }
        {usages.map((i, index) =>
            <tr className="mdc-data-table__row" key={index}>
                <td className="mdc-data-table__cell">{i.id}</td>
                <td className="mdc-data-table__cell"><a href={i.editLink} target="_blank" rel="noreferrer">Edit</a></td>
                <td className="mdc-data-table__cell">{i.name}</td>
            </tr>
        )}
        {
            !!(usages.length && pageSize - usages.length) && [...Array(pageSize - usages.length)].map((_emptyRow, index) =>
                <tr className="mdc-data-table__row" key={index + usages.length}>
                    <td colSpan={5} className="mdc-data-table__cell"></td>
                </tr>)
        }
    </>
};

export default UsageListBody;