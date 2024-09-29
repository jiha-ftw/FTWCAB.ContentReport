import './list.css';
import { useEffect, useState, useCallback } from 'react';
import { useAppSelector, useAppDispatch } from '@/hooks'
import { fetchContentTypeInstances } from '@/store/contentInstance';
import { setSelectedContentInstance } from '@/store/contentUsage'
import UsageList from '@/components/usage-list/usage-list';
import Table from '@/components/table/table';
import ListTableBody from '@/components/list/ListTableBody';
import { ContentInstance } from '@/data/data';

const List = () => {
  const dispatch = useAppDispatch();
  const [currentPage, setCurrentPage] = useState(0);
  const selectedType = useAppSelector(state => state.contentTypes).selectedType;
  const selectedLanguage = useAppSelector(state => state.languages).selectedLanguage;
  const { instances, loaded, pages, totalCount, pageSize } = useAppSelector(state => state.contentInstances);

  const loadInstances = useCallback(() => {
    dispatch(
      fetchContentTypeInstances({
        contentTypeId: selectedType!.id,
        page: currentPage,
        languageId: selectedLanguage?.id || '',
      })
    );
  }, [selectedType, selectedLanguage, currentPage]);

  useEffect(() => {
    setCurrentPage(0);
  }, [selectedType]);

  useEffect(() => {
    selectedType && selectedLanguage && loadInstances();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [selectedType, selectedLanguage, currentPage])
  
  if (!selectedType || !selectedLanguage) return <>Select language and content type to display usage.</>
  if (!loaded) return <></>;

  return (
    <div className="list">

      <UsageList />

      <Table {...{ instances, pageSize, currentPage, setCurrentPage, pages, reload: () => { setCurrentPage(0); loadInstances(); } }} 
        header={<>Instances of: {selectedType.fullName} ({totalCount.toString()} usages)</>}
        tableHeader={<tr>
          <th className="mdc-data-table__header-cell">Id</th>
          <th className="mdc-data-table__header-cell">Edit link</th>
          <th className="mdc-data-table__header-cell" style={{ width: '100%' }}>Name</th>
          <th className="mdc-data-table__header-cell">Usages</th>
          <th className="mdc-data-table__header-cell">Parent link</th>
        </tr>}
        tableBody={<ListTableBody {...{
          items: instances,
          pageSize,
          setSelectedContentInstance: (i: ContentInstance) => dispatch(setSelectedContentInstance(i))
        }} />}
      />

    </div>
  );
}

export default List;
